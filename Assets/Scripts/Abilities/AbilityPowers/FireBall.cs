using System;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.Abilities.AbilityPowers
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float      damage          = 40.0f;
        [SerializeField] private float      projectileSpeed = 10.0f;
        [SerializeField] private LayerMask  collisionIgnoreMask;
        [SerializeField] private GameObject projectile;
        [SerializeField] private GameObject explosion;
        [SerializeField] private Collider   explosionCollider;
        [SerializeField] private Light      lights;
        [SerializeField] private Renderer   ball;

        private bool exploding = false;
        
        // Update is called once per frame
        void Update()
        {
            if (exploding)
            {
                lights.intensity = Mathf.Lerp(lights.intensity, 0, Time.deltaTime * 10.0f);
                return;
            }
            
            transform.position += transform.forward * projectileSpeed*Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (exploding)
            {
                return;
            }
            
            //compare with ignore collision mask
            if (((1 << other.gameObject.layer) & collisionIgnoreMask.value) != 0) return;
            
            Debug.Log($"Hit! {other.gameObject}");
            ball.enabled = false;
            Destroy(projectile.gameObject,0.5f);
            explosion.SetActive(true);
            exploding                 = true;
            explosionCollider.enabled = true;
            Destroy(gameObject, 2.0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log($"Exploding {other.gameObject} for {damage}");
                health.TakeDamage(damage);
            }

        }
    }
}
