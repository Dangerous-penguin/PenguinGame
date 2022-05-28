using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{
    public class Health : MonoBehaviour
    {

        [SerializeField] private float maxHealth       = 100.0f;
        [SerializeField] private float healthRegen     = 0.0f;
        [SerializeField] private float healthRegenTick = 1.0f;

        private float currentHealth;
        private float lastRegenTick;

        private void Start()
        {
            currentHealth = maxHealth;
            lastRegenTick = 0.0f;
        }

        private void Update()
        {
            if (Time.time - lastRegenTick > healthRegenTick)
            {
                currentHealth += healthRegen;
                lastRegenTick =  Time.time;
            }
        }

        public void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
        }
    }
}
