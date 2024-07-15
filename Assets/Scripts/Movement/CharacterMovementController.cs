using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Movement
{
    [RequireComponent(typeof(CharacterController))] //cool thingy
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; //why readonly why not just const?

        [SerializeField] private float speed = 1f;
        [SerializeField] private float maxRadiansDelta = 10f;
        public Vector3 direction { get; set; } //whyyyy you need getter-setter for public field??? uh?
        private CharacterController characterController;

        protected void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (maxRadiansDelta > 0f && direction!= Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = direction * speed * Time.deltaTime;
            characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - direction).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation, 
                    Quaternion.LookRotation(direction, Vector3.up),
                    maxRadiansDelta * Time.deltaTime); //mkey understandable

                transform.rotation = newRotation;
            }
        }
    }
}