using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class State_Patrol : IState
    {
        private FSM _fsm;
        private NavMeshAgent _agent;
        private Transform _transform;
        private Animator _animator;


        public State_Patrol(FSM fsm, NavMeshAgent agent, Transform cachedTransform, Animator animator)
        {
            _fsm = fsm;
            _agent = agent;
            _animator = animator;
            _transform = cachedTransform;
        }

        public void StateUpdate()
        {
        }

        public void OnStateEnter()
        {
            GetNewPosition();
            _agent.isStopped = false;
            _animator.SetBool("IsMoving",true);
        }

        public void OnStateExit()
        {
            _agent.SetDestination(_transform.position);
            _agent.isStopped = true;
            _animator.SetBool("IsMoving",false);
        }

        private void GetNewPosition()
        {
            float randomZ = Random.Range(-10, 10);
            float randomX = Random.Range(-10, 10);

            Vector3 target = new Vector3(randomX, _transform.position.y, randomZ);
            _agent.SetDestination(target);
        }
    }
}
