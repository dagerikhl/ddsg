using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class SelectedInfoBar: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;
        public Transform ActionsContainer;

        [Header("Prefabs")]
        public GameObject ButtonPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void SelectEntity(string title, string description, IEnumerable<SelectedAction> selectedActions) {
            Title.text = title;
            Description.text = description;

            foreach (var selectedAction in selectedActions) {
                var selectedActionButton =
                    Instantiate(ButtonPrefab, ActionsContainer).GetComponent<SelectedActionButton>();
                selectedActionButton.Initialize(selectedAction);
            }
        }

    }

}
