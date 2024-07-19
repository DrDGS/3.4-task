using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.Enemy.States
{
    public class RunAwayState : BaseState
    {
        private readonly EnemyTarget target;
        private readonly EnemyDirectionController enemyDirectionController;

        public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            this.target = target;
            this.enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = target.Closest.transform.position;
            Vector3 runDirection = enemyDirectionController.transform.position - targetPosition;
            enemyDirectionController.UpdateMovementDirectionTrue(runDirection);
        }
    }
}
