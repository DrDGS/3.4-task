using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class Bullet : MonoBehaviour
    {

        public float damage { get; private set; }

        private Vector3 direction;
        private float flySpeed;
        private float maxFlyDistance;
        private float currnetFlyDistnace;

        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float damage)
        {
            this.direction = direction;
            this.flySpeed = flySpeed;
            this.maxFlyDistance = maxFlyDistance;
            this.damage = damage;
        }

        protected void Update()
        {
            var delta = flySpeed * Time.deltaTime;
            currnetFlyDistnace += delta;
            transform.Translate(direction * delta);

            if (currnetFlyDistnace >= maxFlyDistance)
                Destroy(gameObject);
        }
    }
}
