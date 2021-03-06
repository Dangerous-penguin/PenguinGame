using System.Collections;
using System.Collections.Generic;
using DangerousPenguin;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float fillAmount = 1f;
    private MeshRenderer potionMat;

    [SerializeField] private float  fillSmoothing = 40.0f;
    [SerializeField] private Health healthComponent;
    private static readonly  int    FillProperty = Shader.PropertyToID("_fill");

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

    void HealthChanger()
    {
        var current = potionMat.material.GetFloat(FillProperty);
        potionMat.material.SetFloat(FillProperty, Mathf.Lerp(current, fillAmount, Time.deltaTime * fillSmoothing));
    }
}
