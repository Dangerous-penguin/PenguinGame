using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class Patrolling : IState
    {
        private MeeleMinionBB _blackBoard;

        private NavMeshAgent _agent;
        private Transform _transform;

        private Vector3 _target;

        public Patrolling(MeeleMinionBB blackBoard)
        {
            _blackBoard = blackBoard;
            _agent = blackBoard.agent;
            _transform = blackBoard.cachedTransform;
        }

        public void SateUpdate()
        {
            //Check for player
            if ((_transform.position - _target).magnitude < 1) 
            {
                ChangeState(_blackBoard.waitState);
                return;
            }
        }

        public void OnStateEnter()
        {
            GetNewPosition();
        }

        public void OnStateExit()
        {
            _agent.SetDestination(_transform.position);
        }

        public void ChangeState(IState newState)
        {
            _blackBoard.fsm.ChangeState(newState);
        }

        private void GetNewPosition()
        {
            float randomZ = Random.Range(-10, 10);
            float randomX = Random.Range(-10, 10);

            _target = new Vector3(randomX, _transform.position.y, randomZ);
            _agent.SetDestination(_target);
        }
    }
}
