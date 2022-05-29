using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DangerousPenguin
{
    public class EnemyHealth : MonoBehaviour
    {

        private                  float  fillAmount = 1f;
        
        [SerializeField] private Health healthComponent;
        [SerializeField] private Slider slider;


        //[SerializeField] private Health healthComponent;
     


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
            
            if (healthComponent)
            {
                var (cur, max) = healthComponent.CurrentHealth;
                fillAmount     = cur / max;
            }
            
            HealthChanger();
        }

        void HealthChanger (){
            slider.value = 1-fillAmount;
        }
    }
}
