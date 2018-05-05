using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DdSG {

    public class FileClient: SingletonBehaviour<FileClient> {

        protected FileClient() {
        }

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private readonly BinaryFormatter bf = new BinaryFormatter();

        public T LoadFromFile<T>(string filename) {
            var path = Application.persistentDataPath + "/" + filename + Constants.FILE_DATA_EXT;
            var file = File.Open(path, FileMode.Open);

            T data = (T) bf.Deserialize(file);

            file.Close();
            return data;
        }

        public void SaveToFile<T>(string filename, T data) {
            var path = Application.persistentDataPath + "/" + filename + Constants.FILE_DATA_EXT;
            var file = File.Open(path, FileMode.OpenOrCreate);

            bf.Serialize(file, data);

            file.Close();
        }

    }

}
