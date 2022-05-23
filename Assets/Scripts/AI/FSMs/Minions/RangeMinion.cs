using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class RangeMinion : FSM
    {
        //Posibly make a strcut of the most common components
        //Posibly change the conections to be handled by someone and the state by an inteface
        //NEEDS REFACTOR SO THAT I DONT NEED TO MAKE A NEW CLASS

        [field: SerializeField] public FSM fsm { get; private set; }
        [field: SerializeField] public EnemySO enemySO { get; private set; }
        [field: SerializeField] public LayerMask playerLayer { get; private set; }

        public IState waitState { get; private set; }

        [field: SerializeField] public NavMeshAgent agent { get; private set; }
        public Transform cachedTransform { get; private set; }

        private void Awake()
        {
            cachedTransform = transform;

            waitState = new WaitState(this, enemySO, agent, cachedTransform, playerLayer);

            fsm.Init(waitState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, enemySO.aggroRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemySO.attackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemySO.chaseRange);
        }

        private void Update()
        {
            _currentState.StateUpdate();
        }
    }
}
