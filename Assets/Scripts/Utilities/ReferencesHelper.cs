﻿using System.Linq;
using UnityEngine;

namespace DdSG {

    public static class ReferencesHelper {

        public static bool AddReferencesAsAction(StixDataEntityBase entity, ClickableBehaviour clickableBehaviour) {
            ExternalReference[] references = getExternalReferences(entity);
            if (references.Any()) {
                clickableBehaviour.SecondaryAction = () => {
                    foreach (var url in references.Select((e) => e.url)) {
                        if (!GameManager.IsQuickPaused) {
                            HelperObjects.PauseMenu.Pause();
                        }
                        Application.OpenURL(url);
                    }
                };

                return true;
            }

            return false;
        }

        public static void OpenExternalReferences(StixDataEntityBase entity) {
            ExternalReference[] references = getExternalReferences(entity);
            if (references.Any()) {
                foreach (var url in references.Select((e) => e.url)) {
                    if (!GameManager.IsQuickPaused) {
                        HelperObjects.PauseMenu.Pause();
                    }
                    Application.OpenURL(url);
                }
            }
        }

        public static bool HasExternalReferences(StixDataEntityBase entity) {
            return entity.external_references.Any((e) => e.description == null);
        }

        private static ExternalReference[] getExternalReferences(StixDataEntityBase entity) {
            return entity.external_references.Where((e) => e.description == null).ToArray();
        }

    }

}
