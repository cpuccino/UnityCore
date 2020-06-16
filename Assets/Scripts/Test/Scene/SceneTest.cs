using UnityEngine;
using UnityCore.Page;

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
            HandlePage0Input();
            HandlePage1Input();
        }

        void HandlePage0Input()
        {
            if(Input.GetKeyUp(KeyCode.Z))
            {
                _sceneManager.Load(SceneType.Menu);
            }
        }

        void HandlePage1Input()
        {
            if(Input.GetKeyUp(KeyCode.X))
            {
                _sceneManager.Load(SceneType.Level, scene => 
                {
                    Debug.Log($"Scene [{scene}] finished loading");
                }, 
                false, PageType.Loading);
            }
        }
#endif
    }
}
