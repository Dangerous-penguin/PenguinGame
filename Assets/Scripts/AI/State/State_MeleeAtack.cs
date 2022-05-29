using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace DangerousPenguin.AI
{
    public class State_MeleeAtack : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Func<Transform> GetTarget;

        private float _attackTimer;

        public State_MeleeAtack(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Animator animator, Func<Transform> GetTarget)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _animator = animator;
            this.GetTarget = GetTarget;
        }

        public void StateUpdate()
        {
            if(_attackTimer <= 0)
            {
                Debug.Log("Attack"); //add attack system
                _animator.SetTrigger("Attack");
                _attackTimer = _enemySO.attackCooldown;
            }
            else
            {
                _attackTimer -= Time.deltaTime;
            }

        }

        public void OnStateEnter()
        {
            _attackTimer = _enemySO.attackCooldown;
            _agent.isStopped = true;
        }

        public void OnStateExit()
        {
            _agent.isStopped = false;
        }
    }
}
