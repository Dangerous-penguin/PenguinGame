using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    public interface IState
    {
        void StateUpdate();

        void OnStateEnter();

        void OnStateExit();

        void ChangeState(IState newState);
    }
}
