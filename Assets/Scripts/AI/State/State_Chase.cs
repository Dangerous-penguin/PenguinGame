using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace DangerousPenguin.AI
{
    public class State_Chase : IState
    {
        private FSM _fsm;
        private EnemySO _enemySO;
        private NavMeshAgent _agent;
        private Transform _transform;
        private Animator _animator;
        private Func<Transform> GetTarget;

        public State_Chase(FSM fsm, EnemySO enemySO, NavMeshAgent agent, Transform cachedTransform, Animator animator, Func<Transform> GetTarget)
        {
            _fsm = fsm;
            _enemySO = enemySO;
            _agent = agent;
            _transform = cachedTransform;
            _animator = animator;
            this.GetTarget = GetTarget;
        }

        public void StateUpdate()
        {
            _agent.SetDestination(GetTarget().position);
        }

        public void OnStateEnter()
        {
            _agent.stoppingDistance = 1.0f;
            _agent.SetDestination(GetTarget().position);
            _agent.isStopped = false;
            _animator.SetBool("IsMoving",true);
        }

        public void OnStateExit()
        {
            _agent.stoppingDistance = 0;
            _agent.SetDestination(_transform.position);
            _agent.isStopped = true;
            _animator.SetBool("IsMoving",false);
        }
    }
}
