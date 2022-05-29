using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{
    public class Health : MonoBehaviour
    {
        public (float current, float max) CurrentHealth => (currentHealth, maxHealth);
        
        [SerializeField] private float maxHealth       = 100.0f;
        [SerializeField] private float healthRegen     = 0.0f;
        [SerializeField] private float healthRegenTick = 1.0f;
        [SerializeField] private GameObject gameOverCanvas;
        
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
                currentHealth =  Mathf.Clamp(currentHealth + healthRegen, 0, maxHealth);
                lastRegenTick =  Time.time;
            }
        }

        public void TakeDamage(float dmg)
        {
            currentHealth = Mathf.Clamp(currentHealth -dmg, -1, maxHealth);
            if (currentHealth <= 0)
            {
                gameOverCanvas?.SetActive(true);
                Destroy(this.gameObject);
                Debug.LogWarning("You died");
            }
        }
    }
}
