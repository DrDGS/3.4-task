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
        [SerializeField] private float sprintMultiplyer = 2f;
        [SerializeField] private float maxRadiansDelta = 10f;
        public Vector3 movementDirection { get; set; } //whyyyy you need getter-setter for public field??? uh?
        public Vector3 lookDirection { get; set; }
        private CharacterController characterController;

        protected void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (maxRadiansDelta > 0f && lookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = movementDirection * speed * Time.deltaTime * (Input.GetKey(KeyCode.Space) ? sprintMultiplyer : 1f);
            characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - lookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation, 
                    Quaternion.LookRotation(lookDirection, Vector3.up),
                    maxRadiansDelta * Time.deltaTime); //mkey understandable

                transform.rotation = newRotation;
            }
        }
    }
}