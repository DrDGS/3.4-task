using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera camera;
        public Vector3 MovementDirection { get; private set; } //what is the difference between public field with private setter and private field also with private setter?

        protected void Awake()
        {
            camera = UnityEngine.Camera.main; //best way to get the main camera
        }

        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);
            direction = camera.transform.rotation * direction; //wtf multiplying Quaternion by Vector3??? wtf? How does this work?
            //Okay so i read this online - when you multiply quaternion by some vector you are rotating it. So if you have a (0, 45, 0) quaternion rotation (from euler), you can
            //multiply Vector3 by it and thus ROTATE Vector3 by 45 degrees around Y. Really cool and kinda confusing
            direction.y = 0; //cannot fly! sadge

            MovementDirection = direction.normalized;
        }
    }
}