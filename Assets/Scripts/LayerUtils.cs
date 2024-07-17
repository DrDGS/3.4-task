using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);

        //public static readonly int PlayerMask = LayerMask.GetMask(PlayerLayerName);
        //public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);
        public static readonly int CharactersMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);

        public static bool isBullet(GameObject other) => other.layer == BulletLayer;
        public static bool isPickUp(GameObject other) => other.layer == PickUpLayer;
    }
}
