using Assets.Scripts.Shooting;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PickUp
{
    public class PickUpWeapon : MonoBehaviour
    {
        [field: SerializeField] public Weapon weaponPrefab { get; private set; }
    }
}