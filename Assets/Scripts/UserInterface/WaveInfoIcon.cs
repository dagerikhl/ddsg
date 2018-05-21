using TMPro;
using UnityEngine;

namespace DdSG {

    public class WaveInfoIcon: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI Label;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public HoverBehaviour HoverBehaviour;

        // Private and protected members

        private void Awake() {
            HoverBehaviour = GetComponent<HoverBehaviour>();
        }

        public void Initialize(AttackPattern attackPattern) {
            Label.text = attackPattern.name;

            HoverBehaviour.Title = attackPattern.name;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(attackPattern);

            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(attackPattern, HoverBehaviour);
        }

    }

}
