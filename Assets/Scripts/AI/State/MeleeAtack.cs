using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class MeleeAtack : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private LayerMask _playerLayer;
        private Transform _targetPlayer;

        private float _attackTimer;

        public MeleeAtack(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, LayerMask playerLayer, Transform targetPlayer)
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
            if(_attackTimer <= 0)
            {
                Debug.Log("Attack"); //add attack system
                _attackTimer = _enemySO.attackCooldown;
            }
            else
            {
                _attackTimer -= Time.deltaTime;
            }
            if(Vector3.Distance(_transform.position, _targetPlayer.position) > _enemySO.attackRange)
            {
                ChangeState(new ChaseState(_fsm,_enemySO,_agent,_transform,_playerLayer,_targetPlayer));
            }
        }

        public void OnStateEnter()
        {
            _attackTimer = _enemySO.attackCooldown;
        }

        public void OnStateExit()
        {
            _attackTimer = 0;
        }

        public void ChangeState(IState newState)
        {
            _fsm.ChangeState(newState);
        }
    }
}
