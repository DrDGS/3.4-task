using Assets.Scripts.Shooting;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField] public Weapon weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(weaponPrefab);
        }
    }
}