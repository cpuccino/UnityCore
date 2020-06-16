using UnityCore.PersistentUI;
using UnityEngine;

namespace UnityCore.Scene
{
    // Replace with automated tests
    public class SceneTest : MonoBehaviour
    {
        [SerializeField] private SceneManager _sceneManager = default;

        [SerializeField] private SceneType _sceneType0 = default;
        [SerializeField] private SceneType _sceneType1 = default;

#if UNITY_EDITOR

        private void Update()
        {
            HandleScene0Input();
            HandleScene1Input();
        }

        private void HandleScene0Input()
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                _sceneManager.Load(_sceneType0, null, false, PersistentUIType.Loading);
            }
        }

        private void HandleScene1Input()
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                _sceneManager.Load(_sceneType1, null, false, PersistentUIType.Loading);
            }
        }

#endif
    }
}