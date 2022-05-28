using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float fillAmount = 1f;
    private MeshRenderer potionMat;

    // Start is called before the first frame update
    void Start()
    {
        potionMat = GetComponent<MeshRenderer>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthChanger();
    }

    void HealthChanger(){
        potionMat.material.SetFloat("_fill", fillAmount);
    }
}
