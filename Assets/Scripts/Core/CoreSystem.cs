using UnityEngine;
using UnityCore.PersistentUI;

namespace UnityCore
{
    public class CoreSystem : UnityCore.Utilities.SingletonBehaviour<CoreSystem>
    {
        public static UnityCore.Audio.AudioManager AudioManager { get; private set; }

        public static UnityCore.Scene.SceneManager SceneManager { get; private set; }
        
        public static UnityCore.Session.SessionManager SessionManager { get; private set; }

        public static UnityCore.PersistentUI.PersistentUIManager PersistentUIManager { get; private set; }

        public static int TimeScale { get; set; }

        private static bool _persistenceInitialized = false;

        private void Awake()
        {
            Initialize();

            if (!_persistenceInitialized)
            {
                DontDestroyOnLoad(gameObject);
                _persistenceInitialized = true;
            }
            else
            {
                Destroy(gameObject);
            }

            Subscribe();
        }

        private void Initialize()
        {
            AudioManager = UnityCore.Audio.AudioManager.Instance;
            SceneManager = UnityCore.Scene.SceneManager.Instance;
            SessionManager = UnityCore.Session.SessionManager.Instance;
            PersistentUIManager = UnityCore.PersistentUI.PersistentUIManager.Instance;

            TimeScale = 1;
        }

        private void Subscribe()
        {
            SessionManager.OnSessionPaused += HandleOnSessionPaused;
            SessionManager.OnSessionResumed += HandleOnSessionResumed;
        }

        void Unsubscribe()
        {
            SessionManager.OnSessionPaused -= HandleOnSessionPaused;
            SessionManager.OnSessionResumed -= HandleOnSessionResumed;
        }

        void HandleOnSessionPaused()
        {
            Time.timeScale = 0;
            PersistentUIManager.ShowPersistentUI(PersistentUIType.Pause);

            Debug.Log("Session is paused, reverting timeScale to 0");
        }

        void HandleOnSessionResumed()
        {
            Time.timeScale = TimeScale;
            PersistentUIManager.HidePersistentUI(PersistentUIType.Pause);

            Debug.Log($"Session is resumed, reverting timeScale to {TimeScale}");
        }
        
        void OnDestroy() 
        {
            Unsubscribe();
        }
    }
}