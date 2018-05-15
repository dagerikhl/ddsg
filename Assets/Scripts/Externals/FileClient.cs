using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
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
        private string dataPath;

        private void Awake() {
            dataPath = Application.persistentDataPath;
        }

        public bool FileExists(string filename) {
            var path = dataPath + "/" + filename + Constants.FILE_DATA_EXT;
            return File.Exists(path);
        }

        public T LoadFromFile<T>(string filename, bool useBackupPath = false) {
            if (useBackupPath) {
                var asset = Resources.Load<TextAsset>(filename);
                return JsonConvert.DeserializeObject<T>(asset.text);
            }

            var path = dataPath + "/" + filename + Constants.FILE_DATA_EXT;
            var file = File.Open(path, FileMode.Open);

            T data;
            try {
                data = (T) bf.Deserialize(file);
            } catch {
                file.Close();
                throw;
            }

            file.Close();
            return data;
        }

        public void SaveToFile<T>(string filename, T data) {
            var path = dataPath + "/" + filename + Constants.FILE_DATA_EXT;
            var file = File.Open(path, FileMode.OpenOrCreate);

            try {
                bf.Serialize(file, data);
            } catch {
                file.Close();
                throw;
            }

            file.Close();
        }

    }

}
