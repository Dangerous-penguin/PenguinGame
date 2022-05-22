using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    [CreateAssetMenu(fileName = "New Enemy", menuName ="ScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public string test;
        public float waitTimerMax;
        public float waitTimerMin;
    }
}
