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

        [SerializeField] private Health healthComponent;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            if(healthComponent){
                var (cur, max) = healthComponent.CurrentHealth;
                fillAmount = cur/max;
            }
            HealthChanger();
        }

        void HealthChanger (){
            slider.value -= fillAmount;
        }
    }
}
