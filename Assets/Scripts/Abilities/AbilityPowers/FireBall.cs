using System;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.Abilities.AbilityPowers
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float      projectileSpeed = 10.0f;
        [SerializeField] private LayerMask  collisionIgnoreMask;
        [SerializeField] private GameObject projectile;
        [SerializeField] private GameObject explosion;
        [SerializeField] private Collider   explosionCollider;
        [SerializeField] private Light      light;

        private bool exploding = false;
        
        // Update is called once per frame
        void Update()
        {
            if (exploding) return;
            transform.position += transform.forward * projectileSpeed*Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (exploding) return;
            
            //compare with ignore collision mask
            if (((1 << other.gameObject.layer) & collisionIgnoreMask.value) != 0) return;
            
            Debug.Log($"Hit! {other.gameObject}");
            Destroy(projectile.gameObject,0.5f);
            explosion.SetActive(true);
            exploding = true;
            Destroy(gameObject, 2.0f);
        }
    }
}
