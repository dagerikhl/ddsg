using UnityEngine;

namespace DdSG {

    public static class SelectionHelper {

        public static void DeselectAllMitigations() {
            GameObject[] mitigations = GameObject.FindGameObjectsWithTag(Constants.MITIGATION_TAG);
            foreach (var mitigation in mitigations) {
                var mitigationBehaviour = mitigation.GetComponent<MitigationBehaviour>();
                mitigationBehaviour.IsSelected = false;
            }
        }

    }

}
