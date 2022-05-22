using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Input;
using UnityEngine;

namespace DangerousPenguin
{
    public class PlayerCombatController : MonoBehaviour
    {
        private GameControls _input;

        private void Start()
        {
            _input = new GameControls();
            
        }
    }
}
