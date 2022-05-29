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


        //[SerializeField] private Health healthComponent;
     
        void Start()
        {
            slider = GetComponent<Slider>();

        }


        void Update()
        {
          /*  if(healthComponent){
                var (cur, max) = healthComponent.CurrentHealth;
                fillAmount = cur/max;
                
            }*/
            transform.LookAt(Camera.main.transform);
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
