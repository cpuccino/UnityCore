using UnityEngine;
using System;

namespace UnityCore.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> lazyInstance = new Lazy<T>(CreateSingleton);

        public static T Instance => lazyInstance.Value;

        private static T CreateSingleton()
        {
            var gameobject = new GameObject($"{typeof(T).Name} (singleton)");
            var instance = gameobject.AddComponent<T>();
            DontDestroyOnLoad(gameobject);
            return instance;
        }
    }
}