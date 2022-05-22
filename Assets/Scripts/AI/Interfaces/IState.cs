using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    public interface IState
    {
        void SateUpdate();

        void OnStateEnter();

        void OnStateExit();

        void ChangeState(IState newState);
    }
}
