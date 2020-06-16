using UnityEngine;
using UnityCore.Page;

namespace UnityCore.Scene
{
    // Replace with automated tests
    public class SceneTest : MonoBehaviour
    {
        [SerializeField] SceneController sceneController = default;

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
                sceneController.Load(SceneType.Menu);
            }
        }

        void HandlePage1Input()
        {
            if(Input.GetKeyUp(KeyCode.X))
            {
                sceneController.Load(SceneType.Game, scene => 
                {
                    Debug.Log($"Scene [{scene}] finished loading");
                }, 
                false, PageType.Loading);
            }
        }
#endif
    }
}
