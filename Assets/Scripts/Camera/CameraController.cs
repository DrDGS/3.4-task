using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private PlayerCharacter player;
        [SerializeField] private Vector3 followCameraOffset = Vector3.zero;
        [SerializeField] private Vector3 rotationOffset = Vector3.zero;


        protected void Awake()
        {
            if (player == null) 
                throw new NullReferenceException($"CameraController component has a null player reference - {nameof(player)}");
        }


        protected void LateUpdate()
        {
            Vector3 targetRotation = rotationOffset - followCameraOffset;

            transform.position = player.transform.position + followCameraOffset;
            transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        }
    }
}