using System;

namespace UnityCore.Utilities.Serialization
{
    [Serializable]
    public class SaveData
    {
        private static object _singletonLock = new object();
        private static SaveData _instance;

        public static SaveData Current
        {
            get
            {
                lock (_singletonLock)
                {
                    if (_instance != null) return _instance;
                    _instance = new SaveData();
                    return _instance;
                }
            }
        }

        // Initialize values in constructor
        // Replace Current with the appropriate savedata on game load
        private SaveData() { }

        // Add Save data properties
    }
}