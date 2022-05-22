using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class Chasing : IState
    {

        private MeeleMinionBB _blackBoard;
        private Transform _transform;
        private NavMeshAgent _agent;

        private float _checkFollowTimer;
        private float _checkAttackTimer;

        public Chasing(MeeleMinionBB blackBoard)
        {
            _blackBoard = blackBoard;
            _transform = blackBoard.cachedTransform;
            _agent = blackBoard.agent;
        }

        public void SateUpdate()
        {
            if(OutOfRange())
            {
                ChangeState(_blackBoard.patrolState);
                return;
            }
            if(InAttackRange())
            {
                ChangeState(_blackBoard.waitState); //change to attack later
                return;
            }
        }

        public void OnStateEnter()
        {
            _checkFollowTimer = 0;
            _checkAttackTimer = 0;
            _agent.SetDestination(_blackBoard.playerTarget.position);
        }

        public void OnStateExit()
        {
            _checkFollowTimer = 0;
            _checkAttackTimer = 0;
            _agent.SetDestination(_transform.position);
        }

        public void ChangeState(IState newState)
        {
            _blackBoard.fsm.ChangeState(newState);
        }

        private bool OutOfRange()
        {
            if(_checkFollowTimer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position,_blackBoard.enemySO.chaseRange);
                foreach(var colider in colliders)
                {
                    if(colider.CompareTag("Player"))
                    {
                        _checkFollowTimer += 0.35f;
                        return false;
                    }
                }
                return true;
            }
            else
            {
                _checkFollowTimer -= Time.deltaTime;
                return false;
            }
        }

        private bool InAttackRange()
        {
            if (_checkAttackTimer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _blackBoard.enemySO.attackRange);
                foreach (var colider in colliders)
                {
                    if (colider.CompareTag("Player"))
                    {
                        _checkAttackTimer += 0.2f;
                        return false;
                    }
                }
                return true;
            }
            else
            {
                _checkAttackTimer -= Time.deltaTime;
                return false;
            }
        }
    }
}
