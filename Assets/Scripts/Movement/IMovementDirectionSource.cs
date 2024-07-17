using UnityEngine;

namespace Assets.Scripts.Movement
{
    public interface IMovementDirectionSource //actually pretty interesting interface to think of. Can you change its realization to the other one for smth like a cutscene?
    {
        Vector3 MovementDirection { get; }

        bool sprinting { get; }

    }
}
