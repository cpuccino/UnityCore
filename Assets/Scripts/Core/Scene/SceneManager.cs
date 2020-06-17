using System;
using System.Collections;
using System.Threading.Tasks;
using UnityCore.PersistentUI;
using UnityCore.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityCore.Scene
{
    public class SceneManager : SingletonBehaviour<SceneManager>
    {
        [SerializeField] private float _sceneLoadDelay = default;

        private PersistentUIManager _persistentUIManager;
        private PersistentUIType _targetPersistentUIType;

        private SceneType _targetSceneType;
        private bool _sceneIsLoading;

        private Action<SceneType> _onSceneLoadedCallback;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _persistentUIManager = PersistentUIManager.Instance;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private async void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (_targetSceneType == SceneType.None) return;

            var sceneType = (SceneType)Enum.Parse(typeof(SceneType), scene.name);
            if (_targetSceneType != sceneType) return;

            if (_onSceneLoadedCallback != null)
            {
                try
                {
                    _onSceneLoadedCallback(sceneType);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Unable to run the callback after [{sceneType.ToString()}] was loaded. {System.Environment.NewLine}{e.Message}");
                }
            }

            await Task.Delay(Mathf.FloorToInt(_sceneLoadDelay * 1000));

            _persistentUIManager.HidePersistentUI(_targetPersistentUIType);
            _sceneIsLoading = false;
        }

        private IEnumerator LoadScene()
        {
            _persistentUIManager.ShowPersistentUI(_targetPersistentUIType);
            if (_targetPersistentUIType != PersistentUIType.None)
            {
                while (!_persistentUIManager.IsPersistentUIActive(_targetPersistentUIType))
                {
                    yield return null;
                }
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(_targetSceneType.ToString());
        }

        private bool SceneCanBeLoaded(SceneType scene, bool reload)
        {
            if (_sceneIsLoading)
            {
                Debug.LogError($"Unable to load scene [{scene.ToString()}]. Another scene [{_targetSceneType.ToString()}] is in progress.");
                return false;
            }
            if (!Application.CanStreamedLevelBeLoaded(scene.ToString()))
            {
                Debug.LogError($"Invalid scene name [{scene.ToString()}]");
                return false;
            }
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene.ToString() && !reload)
            {
                Debug.LogError($"You are trying to load a scene [{scene.ToString()}] that's currently active");
                return false;
            }

            return true;
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Dispose()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Load(SceneType targetSceneType, Action<SceneType> onSceneLoadedCallback = null, bool reload = false, PersistentUIType loadingPersistentUIType = PersistentUIType.None)
        {
            if (loadingPersistentUIType != PersistentUIType.None && _persistentUIManager == null) return;
            if (!SceneCanBeLoaded(targetSceneType, reload)) return;

            _sceneIsLoading = true;
            _targetSceneType = targetSceneType;
            _targetPersistentUIType = loadingPersistentUIType;
            _onSceneLoadedCallback = onSceneLoadedCallback;

            StartCoroutine(LoadScene());
        }
    }
}