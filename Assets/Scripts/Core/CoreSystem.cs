using UnityEngine;

public class CoreSystem: MonoBehaviour
{
    public static UnityCore.Page.PageManager PageManager { get; private set; }

    public static UnityCore.Audio.AudioManager AudioManager { get; private set; }

    public static UnityCore.Scene.SceneManager SceneManager { get; private set; }

    void Awake()
    {
        PageManager = UnityCore.Page.PageManager.Instance;
        AudioManager = UnityCore.Audio.AudioManager.Instance;
        SceneManager = UnityCore.Scene.SceneManager.Instance;

        DontDestroyOnLoad(gameObject);
    }
}
