using System;
using System.Linq;
using UnityEngine;

// namespace Ukiyo.Unity.Core.Page
// {
//     // Replace with automated tests
//     public class SceneTest : MonoBehaviour
//     {
//         [SerializeField] SceneController sceneController;

//         [SerializeField] SceneType sceneType0;
//         [SerializeField] SceneType sceneType1;

//         #if UNITY_EDITOR
//         void Update()
//         {
//             HandlePageResetInput();
//             HandlePage0Input();
//             HandlePage1Input();
//         }

//         void HandlePage0Input()
//         {
//             if(page0 == null || page0.Type == PageType.None) return;

//             if(Input.GetKeyUp(KeyCode.Q))
//             {
//                 Debug.Log($"Showing {page0.Type.ToString()}");
//                 pageController.ShowPage(page0.Type);
//             }
//             if(Input.GetKeyUp(KeyCode.W))
//             {
//                 Debug.Log($"Hiding {page0.Type.ToString()}");
//                 pageController.HidePage(page0.Type);
//             }
//         }

//         void HandlePage1Input()
//         {
//             if(page1 == null || page1.Type == PageType.None) return;

//             if(Input.GetKeyUp(KeyCode.E))
//             {
//                 Debug.Log($"Hiding {page0.Type.ToString()}, then Showing {page1.Type.ToString()}");
//                 pageController.HidePage(page0.Type, page1.Type);
//             }
//             if(Input.GetKeyUp(KeyCode.R))
//             {
//                 Debug.Log($"Hiding {page0.Type.ToString()}, Wait, then Showing {page1.Type.ToString()}");
//                 pageController.HidePage(page0.Type, page1.Type, true);
//             }
//             if(Input.GetKeyUp(KeyCode.T))
//             {
//                 Debug.Log($"Hiding {page1.Type.ToString()}");
//                 pageController.HidePage(page1.Type);
//             }
//         }
//         #endif
//     }
// }
