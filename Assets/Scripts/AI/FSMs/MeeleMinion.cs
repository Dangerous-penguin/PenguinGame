using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    public class MeeleMinion : FSM
    {

        [SerializeField] private MeeleMinionBB blackBoard;

        private void Update()
        {
            _currentState.SateUpdate();
        }
    }
}
