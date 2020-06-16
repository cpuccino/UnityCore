using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityCore.Utilities.Serialization.Surrogates;
using UnityEngine;

namespace UnityCore.Utilities.Serialization
{
    public class SerializationManager
    {
        static readonly string saveDir = $"{Application.persistentDataPath}/saves";

        public static bool Save(string saveName, object saveData)
        {
            var formatter = GetBinaryFormatter();

            if(!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            string path = $"{saveDir}/{saveName}.sav";

            using(var fs = File.Create(path))
            {
                try
                {
                    formatter.Serialize(fs, saveData);
                    return true;
                }
                catch
                {
                    Debug.LogError($"Failed to save file at path: {path}");
                }
            }

            return true;
        }

        public static object Load(string saveName)
        {
            var formatter = GetBinaryFormatter();
            string path = $"{saveDir}/{saveName}.sav";

            if(!File.Exists(path))
            {
                return null;
            }

            using(var fs = File.Open(path, FileMode.Open))
            {
                try
                {
                    object save = formatter.Deserialize(fs);
                    return save;
                }
                catch
                {
                    Debug.LogError($"Failed to load file at path: {path}");
                    return null;
                }
            }
        }

        public static BinaryFormatter GetBinaryFormatter()
        {
            var formatter = new BinaryFormatter();
            
            formatter.SurrogateSelector = CreateSurrogates();

            return formatter;
        }

        static SurrogateSelector CreateSurrogates()
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