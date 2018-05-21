using UnityEngine;

namespace DdSG {

    public class EnterSystemWaypoint: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private const float minimumDistance = 2f;

        private void Update() {
            GameObject[] attacks = GameObject.FindGameObjectsWithTag(Constants.ATTACK_TAG);
            foreach (GameObject attack in attacks) {
                float distanceToEnemy = Vector3.Distance(transform.position, attack.transform.position);

                if (distanceToEnemy < minimumDistance) {
                    var attackBehaviour = attack.GetComponent<AttackBehaviour>();
                    if (!attackBehaviour.Invulnerable) {
                        Instantiate(HelperObjects.EnteredSystemMessagePrefab, transform);
                        attackBehaviour.Invulnerable = true;
                    }
                }
            }
        }

    }

}
