using Assets.Scripts.Movement;
using Assets.Scripts.Shooting;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public class PlayerCharacter : MonoBehaviour
    {

        private CharacterMovementController characterMovementController;
        private ShootingController shootingController;
        private IMovementDirectionSource movementDirectionSource;

        [SerializeField] private Weapon baseWeaponPrefab;
        [SerializeField] private Transform hand;

        protected void Awake()
        {
            characterMovementController = GetComponent<CharacterMovementController>();
            shootingController= GetComponent<ShootingController>();
            movementDirectionSource = GetComponent<IMovementDirectionSource>();
        }

        protected void Start()
        {
            shootingController.SetWeapon(baseWeaponPrefab, hand);
        }

        protected void Update()
        {
            var direction = movementDirectionSource.MovementDirection;
            characterMovementController.direction = direction;
        }
    }
}
