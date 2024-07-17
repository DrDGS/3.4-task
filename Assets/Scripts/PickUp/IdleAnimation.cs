using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PickUp
{
    public class IdleAnimation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private float levitationIntensivity = 2f;
        [SerializeField] private float levitationOffset = 0.5f;

        private Transform mesh;

        protected void Awake()
        {
            mesh = transform.GetChild(0);
        }

        protected void Update()
        {
            mesh.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.World);
            mesh.position = new Vector3(0, Mathf.Sin(Time.time * levitationIntensivity) * levitationOffset, 0) + transform.position;
        }
    }
}