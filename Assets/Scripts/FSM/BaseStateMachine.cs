using Assets.Scripts.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Scripts.FSM
{
    public class BaseStateMachine
    {
        private BaseState currentState;

        private List<BaseState> states;
        private Dictionary<BaseState, List<Transition>> transitions;

        public BaseStateMachine()
        {
            states = new List<BaseState>();
            transitions = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState (BaseState _state)
        {
            currentState = _state;
        }

        public void AddState(BaseState _state, List<Transition> _transitions)
        {
            if (!states.Contains(_state))
            {
                states.Add(_state);
                transitions.Add(_state, _transitions);
            }
            else
            {
                throw new AlreadyExistsException($"State {_state.GetType()} already exists in state machine!");
            }
        }


        public void Update()
        {
            foreach (var transition in transitions[currentState]) 
            { 
                if (transition.Contidion())
                {
                    currentState = transition.ToState;
                    break;
                }
            }

            currentState.Execute();
        }


        override public string ToString()
        {
            return currentState.GetType().Name;
        }
    }
}
