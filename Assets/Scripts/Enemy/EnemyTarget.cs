using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }

        private readonly float viewRadius;
        private readonly Transform agentTransform;
        private readonly PlayerCharacter player;
        private readonly Collider[] colliders = new Collider[10];

        public EnemyTarget(Transform agent, float viewRadius, PlayerCharacter player)
        {
            agentTransform = agent;
            this.viewRadius = viewRadius;
            this.player = player;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFromAgentTo(Closest);

            return float.MaxValue;
        }

        private float DistanceFromAgentTo(GameObject go) => (agentTransform.position - go.transform.position).magnitude;

        public void FindClosest()
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.CharactersMask);

            for (int i = 0; i < count; i++)
            {
                var go = colliders[i].gameObject;
                if (go == agentTransform.gameObject) continue;
                var distance = DistanceFromAgentTo(go);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                }
            }

            if (player != null && DistanceFromAgentTo(player.gameObject) < minDistance)
                Closest = player.gameObject;
        }

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                agentTransform.position,
                viewRadius,
                colliders,
                layerMask);

            return size;
        }
    }
}
