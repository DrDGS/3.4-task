using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.Enemy.States
{
    internal class FindWayState : BaseState
    {
        private const float MaxDistanceBetweenRealPointAndCalculated = 2f;

        private readonly EnemyTarget target;
        private readonly NavMesher navMesher;
        private readonly EnemyDirectionController enemyDirectionController;

        private Vector3 _currentPoint;

        public FindWayState (EnemyTarget _target, NavMesher _navMesher, EnemyDirectionController _enemyDirectionController)
        {
            target = _target;
            navMesher = _navMesher;
            enemyDirectionController = _enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = target.Closest.transform.position;

            if (!navMesher.IsPathCalculated || navMesher.DistanceToTargetPointFrom(targetPosition) 
                > MaxDistanceBetweenRealPointAndCalculated)
            {
                navMesher.CalculatePath(targetPosition);
            }

            var currentPoint = navMesher.GetCurrentPoint();

            if (_currentPoint != currentPoint)
            {
                _currentPoint = currentPoint;
                enemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }

    }
}
