using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public Bullet bulletPrefab { get; private set; }
        [field: SerializeField] public float shootRadius { get; private set; } = 5f;
        [field: SerializeField] public float shootFrequencySec { get; private set; } = 1f;
        [SerializeField] private float damage = 1f;
        [SerializeField] private float bulletMaxFlyDistance = 10f;
        [SerializeField] private float bulletFlySpeed = 10f;
        [SerializeField] private Transform bulletSpawnPosition;

        public void Shoot(Vector3 targetPoint)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);

            var target = targetPoint - bulletSpawnPosition.position;
            target.y = 0;
            target.Normalize();

            bullet.Initialize(target, bulletMaxFlyDistance, bulletFlySpeed, damage);
        }
    }
}