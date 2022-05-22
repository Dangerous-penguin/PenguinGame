using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class MeeleMinion : FSM
    {
        //Posibly make a strcut of the most common components
        //Change the conections to be handled by someone and the state by an inteface

        [field: SerializeField] public FSM fsm { get; private set; }
        [field: SerializeField] public EnemySO minionSO { get; private set; }
        [field: SerializeField] public LayerMask playerLayer { get; private set; }

        public IState waitState { get; private set; }

        [field: SerializeField] public NavMeshAgent agent { get; private set; }
        public Transform cachedTransform { get; private set; }

        private void Awake()
        {
            cachedTransform = transform;

            waitState = new Waiting(this,minionSO,agent,cachedTransform,playerLayer);

            fsm.Init(waitState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, minionSO.aggroRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minionSO.attackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minionSO.chaseRange);
        }

        private void Update()
        {
            _currentState.SateUpdate();
        }
    }
}
