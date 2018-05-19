﻿using TMPro;
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

        private void Awake() {
            buttonComponent = GetComponent<Button>();
        }

        public void Initialize(SelectedAction selectedAction) {
            Icon.sprite = SpriteManager.I.GetSpriteByType(selectedAction.IconType);
            Label.text = selectedAction.Label;
            buttonComponent.onClick.AddListener(
                () => {
                    if (selectedAction.Action != null) {
                        selectedAction.Action();
                    }
                });
        }

    }

}
