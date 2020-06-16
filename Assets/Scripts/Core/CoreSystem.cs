using UnityCore.Utilities;

public class CoreSystem: Singleton<CoreSystem>
{
    public static UnityCore.Page.PersistentUIManager PersistentUIManager { get; private set; }

    public static UnityCore.Audio.AudioManager AudioManager { get; private set; }

    public static UnityCore.Scene.SceneManager SceneManager { get; private set; }

    private static bool _persistenceInitialized = false;

    void Awake()
    {
        Initialize();
        
        if(!_persistenceInitialized)
        {
            DontDestroyOnLoad(gameObject);
            _persistenceInitialized = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Initialize()
    {
        PersistentUIManager = UnityCore.Page.PersistentUIManager.Instance;
        AudioManager = UnityCore.Audio.AudioManager.Instance;
        SceneManager = UnityCore.Scene.SceneManager.Instance;
    }
}
