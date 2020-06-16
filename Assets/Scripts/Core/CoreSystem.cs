using UnityEngine;

public class CoreSystem: MonoBehaviour
{
    public static UnityCore.Page.PageController PageController { get; private set; }

    public static UnityCore.Audio.AudioController AudioController { get; private set; }

    public static UnityCore.Scene.SceneController SceneController { get; private set; }

    void Awake()
    {
        PageController = UnityCore.Page.PageController.Instance;
        AudioController = UnityCore.Audio.AudioController.Instance;
        SceneController = UnityCore.Scene.SceneController.Instance;

        DontDestroyOnLoad(gameObject);
    }
}
