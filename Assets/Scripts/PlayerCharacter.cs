using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerCharacter : MonoBehaviour
    {

        private CharacterMovementController characterMovementController;
        private IMovementDirectionSource movementDirectionSource;

        void Awake()
        {
            characterMovementController = GetComponent<CharacterMovementController>();
            movementDirectionSource = GetComponent<IMovementDirectionSource>();
        }

        void Update()
        {
            var direction = movementDirectionSource.MovementDirection;
            characterMovementController.direction = direction;
        }
    }
}
