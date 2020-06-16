using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityCore.Utilities.Serialization.Surrogates;
using UnityEngine;

namespace UnityCore.Utilities.Serialization
{
    public class SerializationManager
    {
        public bool Save(string path, string filename, object data)
        {
            var formatter = GetBinaryFormatter();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fs = File.Create(Path.Combine(path, filename)))
            {
                try
                {
                    formatter.Serialize(fs, data);
                    return true;
                }
                catch
                {
                    Debug.LogError($"Failed to save file at path: {path}");
                }
            }

            return true;
        }

        public object Load(string filePath)
        {
            var formatter = GetBinaryFormatter();

            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var fs = File.Open(filePath, FileMode.Open))
            {
                try
                {
                    object save = formatter.Deserialize(fs);
                    return save;
                }
                catch
                {
                    Debug.LogError($"Failed to load file at path: {filePath}");
                    return null;
                }
            }
        }

        private BinaryFormatter GetBinaryFormatter()
        {
            var formatter = new BinaryFormatter();

            formatter.SurrogateSelector = CreateSurrogates();

            return formatter;
        }

        private SurrogateSelector CreateSurrogates()
        {
            var selector = new SurrogateSelector();

            var vector3Surrogate = new Vector3SerializationSurrogate();
            var quaternionSurrogate = new QuaternionSerializationSurrogate();

            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

            return selector;
        }
    }
}