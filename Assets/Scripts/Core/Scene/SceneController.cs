using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Page;
using UnityCore.Utilities;
using System.Threading.Tasks;

namespace UnityCore.Scene
{
    public class SceneController : Singleton<SceneController>
    {
        PageController pageController;

        PageType targetPageType;
        SceneType targetSceneType;
        bool sceneIsLoading;

        Action<SceneType> onSceneLoadedCallback;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {  
            pageController = PageController.Instance;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        async void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if(targetSceneType == SceneType.None) return;

            var sceneType = (SceneType)Enum.Parse(typeof(SceneType), scene.name);
            if(targetSceneType != sceneType) return;

            if(onSceneLoadedCallback != null)
            {
                try 
                {
                    onSceneLoadedCallback(sceneType);
                } 
                catch(Exception e)
                {
                    Debug.LogWarning($"Unable to run the callback after [{sceneType.ToString()}] was loaded. {System.Environment.NewLine}{e.Message}");
                }
            }

            await Task.Delay(1000);

            pageController.HidePage(targetPageType);
            sceneIsLoading = false;
        }

        IEnumerator LoadScene()
        {
            pageController.ShowPage(targetPageType);
            if(targetPageType != PageType.None)
            {
                while(!pageController.IsPageActive(targetPageType))
                {
                    yield return null;
                }
            }
            SceneManager.LoadScene(targetSceneType.ToString());
        }

        bool SceneCanBeLoaded(SceneType scene, bool reload)
        {
            if(sceneIsLoading)
            {
                Debug.LogWarning($"Unable to load scene [{scene.ToString()}]. Another scene [{targetSceneType.ToString()}] is in progress.");
                return false;
            }
            if(!Application.CanStreamedLevelBeLoaded(scene.ToString()))
            {
                Debug.LogWarning($"Invalid scene name [{scene.ToString()}]");
                return false;
            }
            if(SceneManager.GetActiveScene().name == scene.ToString() && !reload)
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
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Load(SceneType targetSceneType, Action<SceneType> onSceneLoadedCallback = null, bool reload = false, PageType loadingPageType = PageType.None)
        {
            if(loadingPageType != PageType.None && pageController == null) return;
            if(!SceneCanBeLoaded(targetSceneType, reload)) return;

            sceneIsLoading = true;
            this.targetSceneType = targetSceneType;
            this.targetPageType = loadingPageType;
            this.onSceneLoadedCallback = onSceneLoadedCallback;

            StartCoroutine(LoadScene());
        }
    }
}
