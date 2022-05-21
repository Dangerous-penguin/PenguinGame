using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DangerousPenguin.demo
{
    public class RandomWalker : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private void Awake()
        {
            StartCoroutine(MoveRandomly());
        }

        private IEnumerator MoveRandomly()
        {
            var random    = Random.insideUnitCircle;
            var direction = new Vector3(random.x, 0.0f, random.y).normalized;
            while (true)
            {
                transform.position += direction * speed * Time.deltaTime;
                if (Mathf.Abs(transform.position.x) > 3)
                {
                    direction.x = -direction.x;
                }

                if (Mathf.Abs(transform.position.z) > 3)
                {
                    direction.z = -direction.z;
                }
                
                yield return null;
            }
        }
    }
}
