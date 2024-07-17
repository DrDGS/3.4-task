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
        [SerializeField] private float centerY = 1f;

        private Transform mesh;

        protected void Awake()
        {
            mesh = transform.GetChild(0); //dumb but we didnt make our prefab universal for any weapon either
        }

        protected void Update()
        {
            mesh.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.World);
            mesh.position = new Vector3(0, Mathf.Sin(Time.time * levitationIntensivity) * levitationOffset, 0) //sin-levitation
                + Vector3.up * centerY //center of our sinus
                + transform.position; //since we are changing global position we should add parent global position
            //maybe it would be better to change local position, however i just thought that using Mathf.Sin like this is kinda easier for now
        }
    }
}