using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class ActionEvents: MonoBehaviour, IPointerClickHandler {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Action PrimaryAction;
        public Action SecondaryAction;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                if (PrimaryAction != null) {
                    PrimaryAction();
                }
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                if (SecondaryAction != null) {
                    SecondaryAction();
                }
            }
        }

    }

}
