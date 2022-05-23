using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class WaitState : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;

        private float _waitTimer;
        private float _checkAgroTimer;

        public WaitState(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, LayerMask playerLayer)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _transform = cachedTransform;
            _playerLayer = playerLayer;
        }

        public void StateUpdate()
        {
            if (_checkAgroTimer <= 0) //Probably make a different class and add it in the bb
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemySO.aggroRange);

                if(colliders.Length > 0)
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

            if(_waitTimer >= 0)
            {
                _waitTimer -= Time.deltaTime;
            }
            else
            {
                ChangeState(new PatrolState(_fsm,_enemySO,_agent,_transform,_playerLayer));
                return;
            }
        }

        public void OnStateEnter()
        {
            _agent.isStopped = true;
            _waitTimer = Random.Range(_enemySO.waitTimerMin,_enemySO.waitTimerMax);
            _checkAgroTimer = 0;
        }

        public void OnStateExit()
        {
            _agent.isStopped = false;
            _checkAgroTimer = 0;
        }

        public void ChangeState(IState newState)
        {
            _fsm.ChangeState(newState);
        }

    }
}
