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


        public State_Wait(FSM fsm, NavMeshAgent agent)
        {
            _fsm = fsm;
            _agent = agent;
        }

        public void StateUpdate()
        {
        }

        public void OnStateEnter()
        {
            _agent.isStopped = true;
        }

        public void OnStateExit()
        {
            _agent.isStopped = false;
        }
    }
}
