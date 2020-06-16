using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.PersistentUI;
using UnityCore.Utilities;
using System.Threading.Tasks;

namespace UnityCore.Scene
{
    public class SceneManager : SingletonBehaviour<SceneManager>
    {
        [SerializeField] float _sceneLoadDelay = default;

        PersistentUIManager _persistentUIManager;

        PersistentUIType _targetPersistentUIType;
        SceneType _targetSceneType;
        bool _sceneIsLoading;

        Action<SceneType> _onSceneLoadedCallback;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {  
            _persistentUIManager = PersistentUIManager.Instance;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        async void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if(_targetSceneType == SceneType.None) return;

            var sceneType = (SceneType)Enum.Parse(typeof(SceneType), scene.name);
            if(_targetSceneType != sceneType) return;

            if(_onSceneLoadedCallback != null)
            {
                try 
                {
                    _onSceneLoadedCallback(sceneType);
                } 
                catch(Exception e)
                {
                    Debug.LogWarning($"Unable to run the callback after [{sceneType.ToString()}] was loaded. {System.Environment.NewLine}{e.Message}");
                }
            }

            await Task.Delay(Mathf.FloorToInt(_sceneLoadDelay * 1000));

            _persistentUIManager.HidePersistentUI(_targetPersistentUIType);
            _sceneIsLoading = false;
        }

        IEnumerator LoadScene()
        {
            _persistentUIManager.ShowPersistentUI(_targetPersistentUIType);
            if(_targetPersistentUIType != PersistentUIType.None)
            {
                while(!_persistentUIManager.IsPersistentUIActive(_targetPersistentUIType))
                {
                    yield return null;
                }
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(_targetSceneType.ToString());
        }

        bool SceneCanBeLoaded(SceneType scene, bool reload)
        {
            if(_sceneIsLoading)
            {
                Debug.LogWarning($"Unable to load scene [{scene.ToString()}]. Another scene [{_targetSceneType.ToString()}] is in progress.");
                return false;
            }
            if(!Application.CanStreamedLevelBeLoaded(scene.ToString()))
            {
                Debug.LogWarning($"Invalid scene name [{scene.ToString()}]");
                return false;
            }
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene.ToString() && !reload)
            {
                Debug.LogWarning($"You are trying to load a scene [{scene.ToString()}] that's currently active");
                return false;
            }

            return true;
        }

        void OnDisable()
        {
            Dispose();
        }

        void Dispose()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Load(SceneType targetSceneType, Action<SceneType> onSceneLoadedCallback = null, bool reload = false, PersistentUIType loadingPersistentUIType = PersistentUIType.None)
        {
            if(loadingPersistentUIType != PersistentUIType.None && _persistentUIManager == null) return;
            if(!SceneCanBeLoaded(targetSceneType, reload)) return;

            _sceneIsLoading = true;
            _targetSceneType = targetSceneType;
            _targetPersistentUIType = loadingPersistentUIType;
            _onSceneLoadedCallback = onSceneLoadedCallback;

            StartCoroutine(LoadScene());
        }
    }
}
