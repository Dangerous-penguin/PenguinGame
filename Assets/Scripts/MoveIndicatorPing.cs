using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{

public class MoveIndicatorPing : MonoBehaviour
{
    private static readonly int ProgressMaterialProperty = Shader.PropertyToID("_Progress");

    [SerializeField] private Renderer mesh;
    [SerializeField] private float    duration = 2.0f;

    private Material _material;
    private float    _elapsedTime = 0;

    private void Start()
    {
        _material = mesh.material;
    }


    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        _material.SetFloat(ProgressMaterialProperty, Mathf.Lerp(0, 5, _elapsedTime / duration));
    }

    void OnDestroy()
    {
        Destroy(_material);
    }
}

}