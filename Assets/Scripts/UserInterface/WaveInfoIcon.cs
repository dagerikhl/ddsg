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
        public ClickableBehaviour ClickableBehaviour;

        // Private and protected members

        private void Awake() {
            ClickableBehaviour = GetComponent<ClickableBehaviour>();
        }

        public void Initialize(AttackPattern attackPattern) {
            Label.text = attackPattern.name;

            ClickableBehaviour.Title = attackPattern.name;
            ClickableBehaviour.Text = Formatter.BuildStixDataEntityDescription(attackPattern);

            ClickableBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(attackPattern, ClickableBehaviour);
        }

    }

}
