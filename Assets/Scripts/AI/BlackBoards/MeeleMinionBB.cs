using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class MeeleMinionBB : MonoBehaviour
    {
        [field:SerializeField] public FSM fsm {get; private set;}
        [field:SerializeField] public MeleeEnemySO enemySO {get; private set;}
        [field: SerializeField] public LayerMask playerLayer { get; private set; }

        public IState patrolState {get; private set;}
        public IState waitState {get; private set;}
        public IState chaseState {get; private set;}

        [field:SerializeField] public NavMeshAgent agent{get; private set;}
        public Transform cachedTransform {get; private set;}

        public Transform playerTarget;

        private void Awake()
        {
            cachedTransform = transform;

            patrolState = new Patrolling(this);
            waitState = new Waiting(this);
            chaseState = new Chasing(this);

            fsm.Init(waitState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, enemySO.aggroRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemySO.attackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,enemySO.chaseRange);
        }
    }
}
