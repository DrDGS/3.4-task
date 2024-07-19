using Assets.Scripts.FSM;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 3f;
        private float runAtHealth;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, 
            NavMesher navMesher, EnemyTarget target, float runAtHealth) 
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var runAwayState = new RunAwayState(target, enemyDirectionController);

            var enemyCharacter = enemyDirectionController.gameObject.GetComponent<BaseCharacter>();

            this.runAtHealth = runAtHealth;

            SetInitialState(idleState);

            AddState(_state: idleState, _transitions: new List<Transition>
                {
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                    new Transition(
                        runAwayState,
                        () => enemyCharacter.health <= enemyCharacter.maxHealth * runAtHealth)
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
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        runAwayState,
                        () => enemyCharacter.health <= enemyCharacter.maxHealth * runAtHealth)
                }
            );
            AddState(_state: runAwayState, _transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                }
            );
        }
    }
}
