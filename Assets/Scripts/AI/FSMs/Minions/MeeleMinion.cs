using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.AI
{
    public class MeeleMinion : MonoBehaviour
    {
        private FSM _fsm;

        [field: SerializeField] public EnemySO enemySO { get; private set;}
        [field: SerializeField] public LayerMask playerLayer { get; private set;}

        [field: SerializeField] public NavMeshAgent agent { get; private set;}
        public Transform cachedTransform { get; private set;}
        public Transform _targetPlayer;

        public float agroCheckTime; //I dont like having these here
        private float _checkAgroTimer;
        
        public float waitTime => UnityEngine.Random.Range(enemySO.waitTimerMin, enemySO.waitTimerMax);
        private float _waitTimer;

        private void Awake()
        {
            _targetPlayer = null;
            cachedTransform = transform;
            _checkAgroTimer = agroCheckTime;
            _waitTimer = waitTime;

            _fsm = new FSM();

            State_Wait waitState = new State_Wait(_fsm,agent);
            State_Patrol patrolState = new State_Patrol(_fsm,agent,cachedTransform);
            State_Chase chaseState = new State_Chase(_fsm,enemySO,agent,cachedTransform,GetPlayer);
            State_MeleeAtack attackState = new State_MeleeAtack(_fsm, enemySO,agent,GetPlayer);

            void AddTransition(IState from, IState to, Func<bool> when) //Move this somewhere else as a static method??
            {
                _fsm.AddTransition(from, to, when);
            }

            AddTransition(waitState,patrolState,CheckWaitTimer);
            AddTransition(waitState,chaseState,() => CheckForPlayer(enemySO.aggroRange));
            AddTransition(patrolState,waitState,() => HaveArrived(0.1f));
            AddTransition(patrolState,chaseState,() => CheckForPlayer(enemySO.aggroRange));
            AddTransition(chaseState,attackState,() => CheckWithinDistance(cachedTransform, _targetPlayer, enemySO.attackRange));
            AddTransition(chaseState,waitState,() => !PlayerInRadius(enemySO.aggroRange));
            AddTransition(attackState,chaseState,() => !CheckWithinDistance(cachedTransform,_targetPlayer,enemySO.attackRange));

            //_fsm.AddAnyStateTransition(chaseState,TEMP);

            bool CheckForPlayer(float range) //Move this somewhere else as a static method??
            {

                _targetPlayer = null;
                if (_checkAgroTimer > 0)
                {
                    _checkAgroTimer -= Time.deltaTime;
                    return false;
                }
                return PlayerInRadius(range);
            }

            bool PlayerInRadius(float range)
            {
                Collider[] colliders = Physics.OverlapSphere(cachedTransform.position, range);
                if (colliders.Length > 0)
                {
                    foreach (var colider in colliders)
                    {
                        if (colider.CompareTag("Player"))
                        {
                            _checkAgroTimer = agroCheckTime;
                            _targetPlayer = colider.transform;
                            return true;
                        }
                    }
                    _checkAgroTimer = agroCheckTime;
                }
                return false;
            }

            bool CheckWaitTimer()
            {
                if (_waitTimer > 0)
                {
                    _waitTimer -= Time.deltaTime;
                    return false;
                }
                _waitTimer = waitTime;
                return true;
            }

            bool HaveArrived(float distance) => agent.remainingDistance <= distance;

            bool CheckWithinDistance(Transform a, Transform b, float distance) => Vector3.Distance(a.position, b.position) <= distance;

            Transform GetPlayer() => _targetPlayer;

            _fsm.ChangeState(waitState);
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
            _fsm.StateUpdate();
        }
    }
}
