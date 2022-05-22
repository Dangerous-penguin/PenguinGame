using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    [CreateAssetMenu(fileName = "New Enemy", menuName ="ScriptableObjects/Enemy")]
    public class MeleeEnemySO : ScriptableObject
    {
        public float waitTimerMin;
        public float waitTimerMax;

        public float attackRange;
        public float aggroRange;
        public float chaseRange;

        public float attackCooldown;
        public float attackDamage;

        public float maxHealth;
    }
}
