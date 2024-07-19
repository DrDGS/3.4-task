using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.Enemy.States
{
    public class MoveForwardState : BaseState
    {
        private readonly EnemyTarget target;
        private readonly EnemyDirectionController enemyDirectionController;

        private Vector3 currentPoint;

        public MoveForwardState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            this.target = target;
            this.enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = target.Closest.transform.position;

            if (currentPoint != targetPosition)
            {
                currentPoint = targetPosition;
                enemyDirectionController.UpdateMovementDirection(targetPosition);
            }
        }

    }
}
