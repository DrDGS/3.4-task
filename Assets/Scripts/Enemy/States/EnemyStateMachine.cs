using Assets.Scripts.FSM;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 2f;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, 
            NavMesher navMesher, EnemyTarget target) 
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);

            SetInitialState(idleState);

            AddState(_state: idleState, _transitions: new List<Transition>
                {
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(_state: findWayState, _transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(_state: moveForwardState, _transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance)
                }
            );
        }
    }
}
