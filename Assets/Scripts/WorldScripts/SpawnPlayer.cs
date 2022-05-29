using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform  spawnPos;

    private GridSystem grid;

    private bool flag = false;

    void Awake()
    {
        grid = FindObjectOfType<GridSystem>();
    }

    // void Update()
    // {
    //     if (grid.isReadyToSpawn == true && !flag)
    //     {
    //         Instantiate(player, spawnPos);
    //         flag = true;
    //     }
    // }
}

}