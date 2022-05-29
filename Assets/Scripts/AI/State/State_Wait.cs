using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class State_Wait : IState
    {
        private FSM _fsm;
        private NavMeshAgent _agent;
        private Animator _animator;

        public State_Wait(FSM fsm, NavMeshAgent agent, Animator animator)
        {
            _fsm = fsm;
            _agent = agent;
            _animator = animator;
        }

        public void StateUpdate()
        {
        }

        public void OnStateEnter()
        {
            _agent.isStopped = true;
            _animator.SetBool("IsMoving",false);
        }

        public void OnStateExit()
        {
            _agent.isStopped = false;
        }
    }
}
