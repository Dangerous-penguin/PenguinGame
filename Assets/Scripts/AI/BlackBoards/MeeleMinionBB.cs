using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class MeeleMinionBB : MonoBehaviour
    {
        [field:SerializeField] public FSM fsm {get; private set;}
        [field:SerializeField] public EnemySO enemySO {get; private set;}

        public IState patrolState { get; private set;}
        public IState waitState { get; private set;}

        [field:SerializeField] public NavMeshAgent agent{get; private set;}
        public Transform cachedTransform {get; private set;}

        private void Awake()
        {
            cachedTransform = transform;

            patrolState = new Patrolling(this);
            waitState = new Waiting(this);

            fsm.Init(waitState);
        }
    }
}
