using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class Bullet : MonoBehaviour
    {

        private Vector3 direction;
        private float flySpeed;
        private float maxFlyDistance;
        private float currnetFlyDistnace;

        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed)
        {
            this.direction = direction;
            this.flySpeed = flySpeed;
            this.maxFlyDistance = maxFlyDistance;
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
