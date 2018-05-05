using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AttackPattern: StixDataEntityBase {

        public AttackPatternCustoms custom;

    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AttackPatternCustoms: TransferObjectBase {

    }

}
