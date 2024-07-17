using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField] private PickUpItem pickUpPrefab;
        [SerializeField] private float range = 2f;
        [SerializeField] private int maxCount = 2;
        [SerializeField] private float spawnIntervalSeconds = 10f;
        
        private float currentSpawnTimeSeconds;
        private int currentCount;

        void Update()
        {
            if (currentCount < maxCount)
            {
                currentSpawnTimeSeconds += Time.deltaTime;
                if (currentSpawnTimeSeconds > spawnIntervalSeconds)
                {
                    currentSpawnTimeSeconds = 0f;
                    currentCount++;

                    var randomPointInsideRange = UnityEngine.Random.insideUnitCircle * range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }


        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }


        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, range);
            Handles.color = cashedColor;
        }
    }
}