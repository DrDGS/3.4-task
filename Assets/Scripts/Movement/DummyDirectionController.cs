using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSource
    {

        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }
    }
}