using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class BackButton: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        [UsedImplicitly]
        public void GoToLastMenu() {
            SceneManager.Instance.GoTo(State.Instance.LastScene);
        }

    }

}
