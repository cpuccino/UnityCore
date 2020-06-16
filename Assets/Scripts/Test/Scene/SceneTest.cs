using UnityEngine;
using UnityCore.Page;

namespace UnityCore.Scene
{
    // Replace with automated tests
    public class SceneTest : MonoBehaviour
    {
        [SerializeField] SceneManager sceneManager = default;

        [SerializeField] SceneType sceneType0 = default;
        [SerializeField] SceneType sceneType1 = default;

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
                sceneManager.Load(SceneType.Menu);
            }
        }

        void HandlePage1Input()
        {
            if(Input.GetKeyUp(KeyCode.X))
            {
                sceneManager.Load(SceneType.Level, scene => 
                {
                    Debug.Log($"Scene [{scene}] finished loading");
                }, 
                false, PageType.Loading);
            }
        }
#endif
    }
}
