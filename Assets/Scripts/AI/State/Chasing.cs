using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class Chasing : IState
    {

        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;
        private Transform _targetPlayer;

        private float _checkFollowTimer;
        private float _checkAttackTimer;

        public Chasing(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, LayerMask playerLayer, Transform targetPlayer)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _transform = cachedTransform;
            _playerLayer = playerLayer;
            _targetPlayer = targetPlayer;
        }

        public void SateUpdate()
        {
            if(OutOfRange())
            {
                ChangeState(new Patrolling(_fsm,_enemySO,_agent,_transform,_playerLayer));
                return;
            }
            if(InAttackRange())
            {
                ChangeState(new Patrolling(_fsm, _enemySO, _agent, _transform, _playerLayer)); //change to attack later
                return;
            }
        }

        public void OnStateEnter()
        {
            _checkFollowTimer = 0;
            _checkAttackTimer = 0;
            _agent.SetDestination(_targetPlayer.position);
        }

        public void OnStateExit()
        {
            _checkFollowTimer = 0;
            _checkAttackTimer = 0;
            _agent.SetDestination(_transform.position);
        }

        public void ChangeState(IState newState)
        {
            _fsm.ChangeState(newState);
        }

        private bool OutOfRange()
        {
            if(_checkFollowTimer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position,_enemySO.chaseRange);
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
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemySO.attackRange);
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
