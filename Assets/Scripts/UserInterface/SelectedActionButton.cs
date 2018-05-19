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
        private HoverBehaviour hoverBehaviour;

        private void Awake() {
            buttonComponent = GetComponent<Button>();
            hoverBehaviour = GetComponent<HoverBehaviour>();
        }

        public void Initialize(SelectedAction selectedAction) {
            Icon.sprite = SpriteManager.I.GetSpriteByActionType(selectedAction.ActionType);
            Label.text = selectedAction.Label;
            if (selectedAction.Action != null) {
                buttonComponent.onClick.AddListener(() => selectedAction.Action());
            }

            hoverBehaviour.Title = selectedAction.Label;
            hoverBehaviour.Text = selectedAction.Description;
            hoverBehaviour.ActionText = selectedAction.Label.ToLower();
        }

    }

}
