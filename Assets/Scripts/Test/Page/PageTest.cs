using System;
using System.Linq;
using UnityEngine;

namespace Ukiyo.Unity.Core.Page
{
    // Replace with automated tests
    public class PageTest : MonoBehaviour
    {
        [SerializeField] PageController pageController;

        [SerializeField] Page page0;
        [SerializeField] Page page1;

        #if UNITY_EDITOR
        void Awake()
        {
            Debug.Log("Page Test Attached");
        }

        void Update()
        {
            HandlePageResetInput();
            HandlePage0Input();
            HandlePage1Input();
        }

        void HandlePageResetInput()
        {
            if(Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Hiding All Pages");
                var pageTypes = Enum.GetValues(typeof(PageType)).Cast<PageType>().ToArray();
                foreach (var pageType in pageTypes)
                {
                    pageController.HidePage(pageType);
                }
            }
        }

        void HandlePage0Input()
        {
            if(page0 == null || page0.Type == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log($"Showing {page0.Type.ToString()}");
                pageController.ShowPage(page0.Type);
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log($"Hiding {page0.Type.ToString()}");
                pageController.HidePage(page0.Type);
            }
        }

        void HandlePage1Input()
        {
            if(page1 == null || page1.Type == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log($"Hiding {page0.Type.ToString()}, then Showing {page1.Type.ToString()}");
                pageController.HidePage(page0.Type, page1.Type);
            }
            if(Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log($"Hiding {page0.Type.ToString()}, Wait, then Showing {page1.Type.ToString()}");
                pageController.HidePage(page0.Type, page1.Type, true);
            }
            if(Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log($"Hiding {page1.Type.ToString()}");
                pageController.HidePage(page1.Type);
            }
        }
        #endif
    }
}
