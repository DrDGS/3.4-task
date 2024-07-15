using Assets.Scripts.Movement;
using Assets.Scripts.Shooting;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public class BaseCharacter : MonoBehaviour
    {

        private CharacterMovementController characterMovementController;
        private ShootingController shootingController;
        private IMovementDirectionSource movementDirectionSource;

        [SerializeField] private Weapon baseWeaponPrefab;
        [SerializeField] private Transform hand;
        [SerializeField] float health = 2f;

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
            var lookDirection = direction;
            if (shootingController.hasTarget)
                lookDirection = (shootingController.targetPosition - transform.position).normalized;
            characterMovementController.movementDirection = direction;
            characterMovementController.lookDirection = lookDirection;

            if (health <= 0f)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.isBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                health -= bullet.damage;
                
                Destroy(other.gameObject);
            }
        }
    }
}
