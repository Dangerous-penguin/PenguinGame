using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    [CreateAssetMenu(fileName = "New Enenmy", menuName = "ScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
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
