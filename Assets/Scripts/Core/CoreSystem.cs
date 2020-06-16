using UnityEngine;

public class CoreSystem: MonoBehaviour
{
    public static UnityCore.Page.PersistentUIManager PersistentUIManager { get; private set; }

    public static UnityCore.Audio.AudioManager AudioManager { get; private set; }

    public static UnityCore.Scene.SceneManager SceneManager { get; private set; }

    void Awake()
    {
        PersistentUIManager = UnityCore.Page.PersistentUIManager.Instance;
        AudioManager = UnityCore.Audio.AudioManager.Instance;
        SceneManager = UnityCore.Scene.SceneManager.Instance;

        DontDestroyOnLoad(gameObject);
    }
}
