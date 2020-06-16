using UnityEngine;

namespace UnityCore.Utilities
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object _singletonLock = new object();
        private static bool _destroyed = false;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_destroyed)
                {
                    Debug.LogWarning($"[Singleton] Instance {typeof(T)} already destroyed. Returning null");
                    return null;
                }

                lock(_singletonLock)
                {
                    var singleton = GetOrCreateSingleton();
                    singleton.name = $"{typeof(T).Name} (Singleton)";
                    return singleton;
                }
            }
        }

        static T GetOrCreateSingleton()
        {
            if(_instance != null) return _instance;

            _instance = (T)FindObjectOfType(typeof(T));
            if(_instance != null) return _instance;

            var gameobject = new GameObject();
            _instance = gameobject.AddComponent<T>();

            return _instance;
        }

        void OnApplicationQuit() 
        {
            _destroyed = true;
        }

        void OnDestroy()
        {
            _destroyed = true;
        }
    }
}