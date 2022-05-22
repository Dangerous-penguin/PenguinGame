using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangerousPenguin.AI
{
    public abstract class FSM : MonoBehaviour
    {
        protected IState _currentState;

        public void Init(IState initialState)
        {
            _currentState = initialState;
            _currentState.OnStateEnter();
        }

        public void ChangeState(IState newState)
        {
            _currentState.OnStateExit();
            _currentState = newState;
            _currentState.OnStateEnter();
        }
    }
}
