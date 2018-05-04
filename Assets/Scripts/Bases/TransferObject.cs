using System;
using UnityEngine;

namespace DdSG {

    [Serializable]
    public abstract class TransferObject {

        public override string ToString() {
            return JsonUtility.ToJson(this, true);
        }

    }

}
