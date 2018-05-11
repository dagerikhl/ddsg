namespace DdSG {

    public class AudioListenerManager: PersistentSingletonBehaviour<AudioListenerManager> {

        protected AudioListenerManager() {
        }

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        protected override string persistentTag { get { return "AudioListener"; } }

    }

}
