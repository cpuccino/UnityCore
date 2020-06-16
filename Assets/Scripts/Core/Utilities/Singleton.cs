using UnityEngine;

namespace UnityCore.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object singletonLock = new object();
        private static bool destroyed = false;
        private static T instance;

        public static T Instance
        {
            get
            {
                if(destroyed)
                {
                    Debug.LogWarning($"[Singleton] Instance {typeof(T)} already destroyed. Returning null");
                    return null;
                }

                lock(singletonLock)
                {
                    var singleton = GetOrCreateSingleton();
                    singleton.name = $"{typeof(T).Name} (Singleton)";
                    return singleton;
                }
            }
        }

        static T GetOrCreateSingleton()
        {
            if(instance != null) return instance;

            instance = (T)FindObjectOfType(typeof(T));
            if(instance != null) return instance;

            var gameobject = new GameObject();
            instance = gameobject.AddComponent<T>();

            return instance;
        }

        void OnApplicationQuit() 
        {
            destroyed = true;
        }

        void OnDestroy()
        {
            destroyed = true;
        }
    }
}