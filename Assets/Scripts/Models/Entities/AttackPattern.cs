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
    public class AttackSteps: TransferObjectBase {

        public AttackStep[] explore;
        public AttackStep[] experiment;
        public AttackStep[] exploit;

    }

    [Serializable]
    public class AttackStep: TransferObjectBase {

        public string title;
        public string[] description;
        public string[] steps;

    }

    [Serializable]
    public class AttackExample: TransferObjectBase {

        public string[] description;
        public ExternalReference[] external_references;

    }

    [Serializable]
    public class AttackMotivation: TransferObjectBase {

        public string[] scope;
        public string[] impact;
        public string[] notes;

    }

    [Serializable]
    public class AttackInjectionVector: TransferObjectBase {

        public string description;
        public string[] category;

    }

    [Serializable]
    public class AttackImpact: TransferObjectBase {

        public string confidentiality;
        public string integrity;
        public string availability;

    }

}
