using System;

namespace UnityCore.Utilities.Serialization
{
    [Serializable]
    public class SaveData
    {
        private static object _singletonLock = new object();
        private static SaveData _instance;

        public static SaveData Instance
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

        private SaveData()
        {
        }

        // Initialize values / save data in the constructor
        // Replace Instance with the the loaded data if it exists
        // Add Save data properties
    }
}