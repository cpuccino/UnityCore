using UnityEngine;
using UnityCore.PersistentUI;

namespace UnityCore.Scene
{
    // Replace with automated tests
    public class SceneTest : MonoBehaviour
    {
        [SerializeField] SceneManager _sceneManager = default;

        [SerializeField] SceneType _sceneType0 = default;
        [SerializeField] SceneType _sceneType1 = default;

#if UNITY_EDITOR
        void Update()
        {
            HandleScene0Input();
            HandleScene1Input();
        }

        void HandleScene0Input()
        {
            if(Input.GetKeyUp(KeyCode.Z))
            {
                _sceneManager.Load(_sceneType0, null, false, PersistentUIType.Loading);
            }
        }

        void HandleScene1Input()
        {
            if(Input.GetKeyUp(KeyCode.X))
            {
                _sceneManager.Load(_sceneType1, null, false, PersistentUIType.Loading);
            }
        }
#endif
    }
}
