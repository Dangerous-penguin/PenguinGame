using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DangerousPenguin
{
    public class EnemyHealth : MonoBehaviour
    {

        private float fillAmount = 1f;
        private Slider slider;
        public GameObject player;

        [SerializeField] private Health healthComponent;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if(healthComponent){
                var (cur, max) = healthComponent.CurrentHealth;
                fillAmount = cur/max;
                transform.LookAt(player.transform);
            }
            if(fillAmount <= 0){
                Destroy(gameObject);
            }
            HealthChanger();
        }

        void HealthChanger (){
            slider.value -= fillAmount;
        }
    }
}
