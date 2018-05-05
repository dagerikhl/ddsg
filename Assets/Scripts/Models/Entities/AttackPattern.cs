using System;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class AttackPattern: StixDataEntityBase {

        public AttackPatternCustoms custom;

    }

    [Serializable]
    public class AttackPatternCustoms: TransferObjectBase {

        public AttackSteps steps;
        public string[] prerequisites;
        public string severity;
        public string likelihood;
        public AttackExample[] examples;
        public string[] probing_techniques;
        public string[] indicators;
        public AttackMotivation[] motivations;
        public AttackInjectionVector injection_vector;
        public string payload;
        public string activation_zone;
        public AttackImpact impact;

    }

    [Serializable]
    public class AttackSteps {

    }

    [Serializable]
    public class AttackExample {

    }

    [Serializable]
    public class AttackMotivation {

    }

    [Serializable]
    public class AttackInjectionVector {

    }

    [Serializable]
    public class AttackImpact {

    }

}
