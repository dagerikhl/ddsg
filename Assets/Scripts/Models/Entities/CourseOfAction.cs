using System;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class CourseOfAction: StixDataEntityBase {

        public CourseOfActionCustoms custom;

    }

    [Serializable]
    public class CourseOfActionCustoms: TransferObjectBase {

        public string category;
        public string mitigation;

    }

}
