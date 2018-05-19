using System;
using UnityEngine;

namespace DdSG {

    public class SpriteManager: SingletonBehaviour<SpriteManager> {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Sprite SellIcon;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public Sprite GetSpriteByType(SpriteType type) {
            switch (type) {
            case SpriteType.Sell:
                return SellIcon;
            default:
                throw new ArgumentOutOfRangeException("type", type, "The SpriteType doesn't exist.");
            }
        }

    }

}
