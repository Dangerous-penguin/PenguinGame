using System;
using UnityEngine;
using UnityEngine.AI;

namespace DangerousPenguin.Abilities.AbilityPowers
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float     projectileSpeed = 10.0f;
        [SerializeField] private LayerMask collisionMask;

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.forward * projectileSpeed*Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(LayerMask.)
            Debug.Log("Hit!");
        }
    }
}
