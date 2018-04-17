using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class SelectLevelButton: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Text LevelText;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public int LevelNumber;

        // Private members

        private void Start() {
            LevelNumber = int.Parse(Regex.Match(name, @"^Level \((\d+)\)").Groups[1].Value);
            LevelText.text = string.Format("Level\n{0}", LevelNumber);
        }

    }

}
