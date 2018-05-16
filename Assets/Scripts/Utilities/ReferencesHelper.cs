using System.Linq;
using UnityEngine;

namespace DdSG {

    public static class ReferencesHelper {

        public static bool AddReferencesAsAction(StixDataEntityBase entity, ActionEvents actionEvents) {
            ExternalReference[] references = entity.external_references.Where((e) => e.description == null).ToArray();
            if (references.Any()) {
                actionEvents.SecondaryAction = () => {
                    foreach (var url in references.Select((e) => e.url)) {
                        HelperObjects.PauseMenu.Pause();
                        Application.OpenURL(url);
                    }
                };

                return true;
            }

            return false;
        }

    }

}
