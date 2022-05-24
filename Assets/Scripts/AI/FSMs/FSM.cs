using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DangerousPenguin.AI
{
    public class FSM
    {
        public IState _currentState;

        private Dictionary<Type, List<Transitions>> _transitionsDic = new Dictionary<Type, List<Transitions>>();
        private List<Transitions> _currentTransitions = new List<Transitions>();
        private List<Transitions> _anyStateTransitions = new List<Transitions>();

        private static List<Transitions> EmptyTransitionsList = new List<Transitions>(0);

        public void StateUpdate()
        {
            Transitions transition = GetTransition();
            if(transition != null)
            {
                ChangeState(transition.nextState);
            }
            _currentState?.StateUpdate();
        }

        public void ChangeState(IState newState)
        {
            if(newState == _currentState) return;
            
            _currentState?.OnStateExit();
            _currentState = newState;

            _transitionsDic.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if(_currentTransitions == null) 
            {
                _currentTransitions = EmptyTransitionsList;
            }

            _currentState.OnStateEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> when)
        {
            if(_transitionsDic.TryGetValue(from.GetType(), out List<Transitions> transitions) == false)
            {
                transitions = new List<Transitions>();
                _transitionsDic[from.GetType()] = transitions;
            }
            transitions.Add(new Transitions(to, when));
        }

        public void AddAnyStateTransition(IState to, Func<bool> when)
        {
            _anyStateTransitions.Add(new Transitions(to,when));
        }

        private class Transitions
        {
            public Func<bool> Condition {get; }
            public IState nextState {get; }

            public Transitions(IState next, Func<bool> condition)
            {
                Condition = condition;
                nextState = next;
            }
        }

        private Transitions GetTransition()
        {
            foreach(Transitions transition in _anyStateTransitions)
            {
                if(transition.Condition())
                {
                    return transition;
                }
            }

            foreach(Transitions transition in _currentTransitions)
            {
                if(transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        }
    }
}
