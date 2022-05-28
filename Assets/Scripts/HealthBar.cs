using System.Collections;
using System.Collections.Generic;
using DangerousPenguin;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float fillAmount = 1f;
    private MeshRenderer potionMat;

    [SerializeField] private Health healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        potionMat = GetComponent<MeshRenderer>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthComponent)
        {
            var (cur, max) = healthComponent.CurrentHealth;
            fillAmount     = cur / max;
        }

        HealthChanger();
    }

    void HealthChanger(){
        potionMat.material.SetFloat("_fill", fillAmount);
    }
}
