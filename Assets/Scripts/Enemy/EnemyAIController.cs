using Assets.Scripts.Enemy.States;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private float viewRadius = 100f;
        [SerializeField] private float runAtHealthToMaxHealthRatio = 0.5f;
        private EnemyStateMachine stateMachine;
        private EnemyTarget target;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var navMesher = new NavMesher(transform);

            target = new EnemyTarget(transform, viewRadius, player);

            stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, target, runAtHealthToMaxHealthRatio);
        }

        protected void Update()
        {
            target.FindClosest();
            stateMachine.Update();
            Debug.Log(stateMachine.ToString());
        }

        protected void OnDrawGizmosSelected()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, viewRadius);
            Handles.color = cashedColor;
        }
    }
}