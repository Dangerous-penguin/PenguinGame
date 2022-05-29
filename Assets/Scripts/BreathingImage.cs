using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{

public class BreathingImage : MonoBehaviour
{
    [SerializeField] private float baseScale = 9.0f;

    // Update is called once per frame
    void Update()
    {
        float scale = baseScale - 0.25f + Mathf.Sin(Time.time) / 4.0f;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}

}