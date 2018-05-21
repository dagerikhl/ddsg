using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class SelectedActionButton: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI Label;
        public Image Icon;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private Button buttonComponent;
        private ClickableBehaviour clickableBehaviour;

        private void Awake() {
            buttonComponent = GetComponent<Button>();
            clickableBehaviour = GetComponent<ClickableBehaviour>();
        }

        public void Initialize(SelectedAction selectedAction) {
            Icon.sprite = SpriteManager.I.GetSpriteByActionType(selectedAction.ActionType);
            Label.text = selectedAction.Label;
            if (selectedAction.Action != null) {
                buttonComponent.onClick.AddListener(() => selectedAction.Action());
            }

            clickableBehaviour.Title = selectedAction.Label;
            clickableBehaviour.Text = selectedAction.Description;
            clickableBehaviour.ActionText = selectedAction.Label.ToLower();
        }

    }

}
