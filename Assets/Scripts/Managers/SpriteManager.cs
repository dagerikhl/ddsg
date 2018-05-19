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

        public Sprite GetSpriteByActionType(ActionType type) {
            switch (type) {
            case ActionType.Sell:
                return SellIcon;
            default:
                throw new ArgumentOutOfRangeException("type", type, "The action type doesn't exist.");
            }
        }

    }

}
