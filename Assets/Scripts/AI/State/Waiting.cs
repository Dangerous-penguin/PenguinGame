using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class Waiting : IState
    {

        private MeeleMinionBB _blackBoard;
        private MeleeEnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;

        private float _waitTimer;
        private float _checkAgroTimer;

        public Waiting(MeeleMinionBB blackBoard)
        {
            _blackBoard = blackBoard;
            _enemySO = blackBoard.enemySO;
            _agent = blackBoard.agent;
            _transform = blackBoard.cachedTransform;
            _playerLayer = blackBoard.playerLayer;
        }

        public void SateUpdate()
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

            if(_waitTimer >= 0)
            {
                _waitTimer -= Time.deltaTime;
            }
            else
            {
                ChangeState(_blackBoard.patrolState);
                return;
            }
        }

        public void OnStateEnter()
        {
            _agent.isStopped = true;
            MeleeEnemySO enemySO = _blackBoard.enemySO;
            _waitTimer = Random.Range(enemySO.waitTimerMin,enemySO.waitTimerMax);
            _checkAgroTimer = 0;
        }

        public void OnStateExit()
        {
            _agent.isStopped = false;
            _checkAgroTimer = 0;
        }

        public void ChangeState(IState newState)
        {
            _blackBoard.fsm.ChangeState(newState);
        }

    }
}
