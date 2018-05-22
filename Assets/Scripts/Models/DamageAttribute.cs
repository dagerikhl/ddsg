using System;

namespace DdSG {

    [Serializable]
    public class DamageAttribute {

        public float MinimumDamage;
        public float MaximumDamage;

        public float GetAverageDamage() {
            return (MinimumDamage + MaximumDamage)/2f;
        }

        public float GetDamagePerSecond(float fireRate) {
            return GetAverageDamage()*fireRate;
        }

    }

}
