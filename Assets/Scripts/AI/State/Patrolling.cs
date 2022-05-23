using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class PatrolState : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;

        private Vector3 _target;
        private float _checkAgroTimer;

        public PatrolState(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, LayerMask playerLayer)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _transform = cachedTransform;
            _playerLayer = playerLayer;
        }

        public void StateUpdate()
        {
            if(_checkAgroTimer <= 0) //Probably make a different class and call a method
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemySO.aggroRange);

                if (colliders.Length > 0)
                {
                    foreach (var colider in colliders)
                    {
                        if (colider.CompareTag("Player"))
                        {
                            ChangeState(new ChaseState(_fsm,_enemySO,_agent,_transform,_playerLayer,colider.transform));
                            return;
                        }
                    }
                }
                _checkAgroTimer += 0.35f;
            }
            else
            {
                _checkAgroTimer -= Time.deltaTime;
            }

            if (Vector3.Distance(_transform.position, _target) < 1) 
            {
                ChangeState(new WaitState(_fsm,_enemySO,_agent,_transform,_playerLayer));
                return;
            }
        }

        public void OnStateEnter()
        {
            GetNewPosition();
        }

        public void OnStateExit()
        {
            _agent.SetDestination(_transform.position);
        }

        public void ChangeState(IState newState)
        {
            _fsm.ChangeState(newState);
        }

        private void GetNewPosition()
        {
            float randomZ = Random.Range(-10, 10);
            float randomX = Random.Range(-10, 10);

            _target = new Vector3(randomX, _transform.position.y, randomZ);
            _agent.SetDestination(_target);
        }
    }
}
