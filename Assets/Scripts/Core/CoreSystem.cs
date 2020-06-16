using UnityCore.Utilities;

public class CoreSystem : SingletonBehaviour<CoreSystem>
{
    public static UnityCore.PersistentUI.PersistentUIManager PersistentUIManager { get; private set; }

    public static UnityCore.Audio.AudioManager AudioManager { get; private set; }

    public static UnityCore.Scene.SceneManager SceneManager { get; private set; }

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
    }

    private void Initialize()
    {
        PersistentUIManager = UnityCore.PersistentUI.PersistentUIManager.Instance;
        AudioManager = UnityCore.Audio.AudioManager.Instance;
        SceneManager = UnityCore.Scene.SceneManager.Instance;
    }
}