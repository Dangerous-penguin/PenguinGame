using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class ChaseState : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;
        private Transform _targetPlayer;

        private float _checkFollowTimer;

        public ChaseState(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, LayerMask playerLayer, Transform targetPlayer)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _transform = cachedTransform;
            _playerLayer = playerLayer;
            _targetPlayer = targetPlayer;
        }

        public void StateUpdate()
        {
            _agent.SetDestination(_targetPlayer.position);
            CheckOutOfRange();
        }

        public void OnStateEnter()
        {
            _checkFollowTimer = 0;
            _agent.stoppingDistance = _enemySO.attackRange;
            _agent.SetDestination(_targetPlayer.position);
        }

        public void OnStateExit()
        {
            _checkFollowTimer = 0;
            _agent.stoppingDistance = 0;
            _agent.SetDestination(_transform.position);
        }

        public void ChangeState(IState newState)
        {
            _fsm.ChangeState(newState);
        }

        private void CheckOutOfRange()
        {
            if(_checkFollowTimer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position,_enemySO.chaseRange);
                foreach(var collider in colliders)
                {
                    if(collider.CompareTag("Player"))
                    {
                        CheckAttackRange(collider.transform.position);
                        return;
                    }
                }
                ChangeState(new PatrolState(_fsm, _enemySO, _agent, _transform, _playerLayer));
                return;
            }
            else
            {
                _checkFollowTimer -= Time.deltaTime;
            }
        }

        private void CheckAttackRange(Vector3 targetPosition)
        {
            if (_agent.remainingDistance <= _enemySO.attackRange)
            {
                ChangeState(new MeleeAtack(_fsm, _enemySO, _agent, _transform, _playerLayer, _targetPlayer));
                return;
            }
        }
    }
}
