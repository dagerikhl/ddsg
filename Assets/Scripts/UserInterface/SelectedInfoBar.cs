using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class SelectedInfoBar: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Type;
        public Transform ActionsContainer;

        [Header("Prefabs")]
        public GameObject ButtonPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        private void Awake() {
            Deselect();
        }

        public void SelectEntity(
            string title,
            string type,
            string description,
            IEnumerable<SelectedAction> selectedActions = null) {
            Title.text = title;
            Type.text = string.Format("Selected {0}", type);
            Description.text = description;

            ActionsContainer.Clear();

            if (selectedActions != null) {
                foreach (var selectedAction in selectedActions) {
                    var selectedActionButton =
                        Instantiate(ButtonPrefab, ActionsContainer).GetComponent<SelectedActionButton>();
                    selectedActionButton.Initialize(selectedAction);
                }
            }
        }

        public void Deselect() {
            Title.text = "";
            Description.text = "";
            Type.text = "Nothing selected";

            ActionsContainer.Clear();
        }

    }

}
