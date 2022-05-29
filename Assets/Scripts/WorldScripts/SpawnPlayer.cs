using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{
    public class SpawnPlayer : MonoBehaviour
    {
        public GameObject Player;
        public Transform SpawnPos;
        public GridSystem grid;

        private bool flag = false;

        void Awake() {
            grid = FindObjectOfType<GridSystem>();
        }
        void Update()
        {
            if(grid.isReadyToSpawn == true && !flag){
                Instantiate(Player, SpawnPos);
                flag = true;
            }
        }
    }
}
