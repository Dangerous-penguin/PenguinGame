using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DangerousPenguin
{

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MeeleMinion prefab;
    [SerializeField] private int         count;
    [SerializeField] private float       radius = 5.0f;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var random = Random.onUnitSphere * radius;
            var enemy  = Instantiate(prefab, transform);
            random.y = 0;

            enemy.transform.localPosition = random;
            enemy.transform.rotation      = Quaternion.Euler(0, Random.value*360.0f, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        
        for (int i = 0; i < count; i++)
        {
            var pos = transform.position;
            pos.y += 2.0f;
            pos.x += ((i - (count) / 2) * 2f);
            Gizmos.DrawSphere(pos, 0.7f);
        }
        
    }
}

}