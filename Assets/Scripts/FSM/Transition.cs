using System;

namespace Assets.Scripts.FSM
{
    public class Transition
    {
        public BaseState ToState { get; }
        public Func<bool> Contidion { get; }

        public Transition(BaseState toState, Func<bool> contidion)
        {
            ToState = toState;
            Contidion = contidion;
        }
    }
}
