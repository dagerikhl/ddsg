﻿using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class BackButton: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        [UsedImplicitly]
        public void GoToLastMenu() {
            SceneManager.I.GoToLastMenu();
        }

    }

}
