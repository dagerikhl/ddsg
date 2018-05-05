using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CourseOfAction: StixDataEntityBase {

        public CourseOfActionCustoms custom;

    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CourseOfActionCustoms: TransferObjectBase {

        public string category;
        public string mitigation;

    }

}
