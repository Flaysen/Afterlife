using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Enemies
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private Dictionary<Type, BaseState> _availableStates;
        public event Action<BaseState> OnStateChanged;
        public BaseState CurrentState { get; private set; }
        
        public void SetStates(Dictionary<Type, BaseState> states)
        {
            _availableStates = states;
        }
        private void Update()
        {
            if(CurrentState == null)
            {
                CurrentState = _availableStates.Values.First();
            }

            var nextState = CurrentState?.StateUpdate();

            if(nextState != null &&
                nextState != CurrentState?.GetType())
            {
                SwitchToNewState(nextState);
            }
        }
        private void SwitchToNewState(Type nextState)
        {
            CurrentState = _availableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}



