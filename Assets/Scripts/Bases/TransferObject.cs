using System;
using Newtonsoft.Json;

namespace DdSG {

    [Serializable]
    public abstract class TransferObject {

        public override string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }

}
