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

        [SerializeField] private bool showGizmos = false;

        [field: SerializeField] public EnemySO enemySO { get; private set;}
        [field: SerializeField] public LayerMask playerLayer { get; private set;}

        [field: SerializeField] public NavMeshAgent agent { get; private set;}
        [field: SerializeField] public Animator animator { get; private set;}
        public Transform cachedTransform { get; private set;}
        public Transform _targetPlayer;

        public float agroCheckTime; //I dont like having these here maybe passing func from interfaces
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

            State_Wait waitState = new State_Wait(_fsm,agent,animator);
            State_Patrol patrolState = new State_Patrol(_fsm,agent,cachedTransform,animator);
            State_Chase chaseState = new State_Chase(_fsm,enemySO,agent,cachedTransform,animator,GetPlayer);
            State_MeleeAtack attackState = new State_MeleeAtack(_fsm, enemySO,agent,animator,GetPlayer);

            void AddTransition(IState from, IState to, Func<bool> when) //Move this somewhere else as a static method??
            {
                _fsm.AddTransition(from, to, when);
            }

            AddTransition(waitState,patrolState,CheckWaitTimer);
            AddTransition(waitState,chaseState,() => CheckForPlayer(enemySO.aggroRange));
            AddTransition(patrolState,waitState,() => HaveArrived(0.1f));
            AddTransition(patrolState,chaseState,() => CheckForPlayer(enemySO.aggroRange));
            AddTransition(chaseState, waitState, () => _targetPlayer == null);
            AddTransition(chaseState,attackState,() => CheckWithinDistance(cachedTransform, _targetPlayer, enemySO.attackRange-1.0f));
            AddTransition(chaseState,waitState,() => !PlayerInRadius(enemySO.aggroRange));
            AddTransition(attackState, waitState, () => _targetPlayer == null);
            AddTransition(attackState,chaseState,() => !CheckWithinDistance(cachedTransform,_targetPlayer,enemySO.attackRange));

            //_fsm.AddAnyStateTransition(chaseState,TEMP);

            bool CheckForPlayer(float range) //Move this somewhere else as a static method or come from another interface/class
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
            if (!showGizmos && !Application.isPlaying && true) return;
            if(showGizmos || _fsm._currentState.GetType() == typeof(State_Patrol) || _fsm._currentState.GetType() == typeof(State_Wait))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, enemySO.aggroRange);
            }
            if (showGizmos || _fsm._currentState.GetType() == typeof(State_Chase) || _fsm._currentState.GetType() == typeof(State_MeleeAtack))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, enemySO.attackRange);
            }
            if (showGizmos || _fsm._currentState.GetType() == typeof(State_Chase))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, enemySO.chaseRange);
            }
        }

        private void Update()
        {
            _fsm.StateUpdate();
        }
    }
}
