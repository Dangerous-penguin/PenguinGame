using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin
{
    public class EyeSwapper : MonoBehaviour
    {
        [SerializeField] private Material glowyEyes;
        [SerializeField] private Renderer eyeRenderer;

        public void GlowThemEyes()
        {
            var mats = eyeRenderer.materials; 
            mats[2]               = glowyEyes;
            eyeRenderer.materials = mats;

        }
    }
}
