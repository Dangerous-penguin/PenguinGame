using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class Patrolling : IState
    {
        
        private MeeleMinionBB _blackBoard;
        private MeleeEnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;

        private Vector3 _target;
        private float _checkAgroTimer;

        public Patrolling(MeeleMinionBB blackBoard)
        {
            _blackBoard = blackBoard;
            _enemySO = blackBoard.enemySO;
            _agent = blackBoard.agent;
            _transform = blackBoard.cachedTransform;
            _playerLayer = blackBoard.playerLayer;
        }

        public void SateUpdate()
        {
            if(_checkAgroTimer <= 0) //Probably make a different class and add it in the bb
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemySO.aggroRange);

                if (colliders.Length > 0)
                {
                    foreach (var colider in colliders)
                    {
                        if (colider.CompareTag("Player"))
                        {
                            _blackBoard.playerTarget = colider.transform;
                            ChangeState(_blackBoard.chaseState);
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
                ChangeState(_blackBoard.waitState);
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
            _blackBoard.fsm.ChangeState(newState);
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
