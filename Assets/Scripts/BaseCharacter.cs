using Assets.Scripts.Movement;
using Assets.Scripts.PickUp;
using Assets.Scripts.Shooting;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {

        private CharacterMovementController characterMovementController;
        private ShootingController shootingController;
        private IMovementDirectionSource movementDirectionSource;

        [SerializeField] private Weapon baseWeaponPrefab;
        [SerializeField] private Transform hand;
        [SerializeField] float health = 2f;

        private float buffTimeRemaining = 0f;
        private float speedBuffMultiplyer = 1f;

        protected void Awake()
        {
            characterMovementController = GetComponent<CharacterMovementController>();
            shootingController= GetComponent<ShootingController>();
            movementDirectionSource = GetComponent<IMovementDirectionSource>();
        }

        protected void Start()
        {
            shootingController.SetWeapon(baseWeaponPrefab, hand);
            SetWeapon(baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (shootingController.hasTarget)
                lookDirection = (shootingController.targetPosition - transform.position).normalized;
            var sprinting = movementDirectionSource.sprinting;

            characterMovementController.movementDirection = direction;
            characterMovementController.lookDirection = lookDirection;
            characterMovementController.sprinting = sprinting;

            if (buffTimeRemaining > 0f) buffTimeRemaining -= Time.deltaTime;
            else
            {
                speedBuffMultiplyer = 1f;
                characterMovementController.buffMultiplyer = speedBuffMultiplyer;
            }

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
            else if (LayerUtils.isPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            shootingController.SetWeapon(weapon, hand);
        }

        public void GetBuff(BuffPack buffPrefab)
        {
            buffTimeRemaining += buffPrefab.buffTime;
            speedBuffMultiplyer = buffPrefab.buffTime;
            characterMovementController.buffMultiplyer = speedBuffMultiplyer;
        }
    }
}
