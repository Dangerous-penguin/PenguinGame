using UnityEngine;

namespace DangerousPenguin
{
    public class ExitStairs : MonoBehaviour
    {

        private LevelManager level;

        void Start()
        {
            level = FindObjectOfType<LevelManager>();
        }


        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
                NewLevel();
            }
        }

        public void NewLevel(){
            if(level == null){
                level.BeginGame();
            }
            else{
                level.DestroyLevel();
                level.BeginGame();
            }
        }
    }
}
