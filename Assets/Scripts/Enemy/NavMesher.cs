using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class NavMesher
    {
        private const float DistanceEps = 1f;

        public bool IsPathCalculated { get; private set; }

        private readonly NavMeshQueryFilter filter;
        private readonly Transform agentTransform;

        private NavMeshPath navMeshPath;
        private NavMeshHit targetHit;
        private int currentPathPointIndex;

        public NavMesher(Transform agentTransform)
        {
            filter = new NavMeshQueryFilter {
                areaMask = NavMesh.AllAreas
            };
            IsPathCalculated = false;

            navMeshPath = new NavMeshPath();
            this.agentTransform = agentTransform;
        }

        public void CalculatePath(Vector3 targetPosition)
        {
            NavMesh.SamplePosition(agentTransform.position, out var agentHit, 10f, filter);
            NavMesh.SamplePosition(targetPosition, out targetHit, 10f, filter);
            IsPathCalculated = NavMesh.CalculatePath(agentHit.position, targetHit.position, filter, navMeshPath);
            currentPathPointIndex = 0;
        }

        public Vector3 GetCurrentPoint()
        {
            var currentPoint = navMeshPath.corners[currentPathPointIndex];
            var distance = (agentTransform.position - currentPoint).magnitude;

            if (distance < DistanceEps)
            {
                currentPathPointIndex++;
            }

            if (currentPathPointIndex >= navMeshPath.corners.Length)
                IsPathCalculated = false;
            else
            {
                currentPoint = navMeshPath.corners[currentPathPointIndex];
            }

            return currentPoint;
        }

        public float DistanceToTargetPointFrom(Vector3 position) => (targetHit.position - position).magnitude;
    }
}
