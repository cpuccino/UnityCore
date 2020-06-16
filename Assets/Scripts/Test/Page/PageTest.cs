using System;
using System.Linq;
using UnityEngine;

namespace UnityCore.Page
{
    // Replace with automated tests
    public class PageTest : MonoBehaviour
    {
        [SerializeField] PersistentUIManager _persistentUIManager = default;

        [SerializeField] PageType _pageType0 = default;
        [SerializeField] PageType _pageType1 = default;

#if UNITY_EDITOR
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
                    _persistentUIManager.HidePage(pageType);
                }
            }
        }

        void HandlePage0Input()
        {
            if(_pageType0 == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log($"Showing {_pageType0.ToString()}");
                _persistentUIManager.ShowPage(_pageType0);
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log($"Hiding {_pageType0.ToString()}");
                _persistentUIManager.HidePage(_pageType0);
            }
        }

        void HandlePage1Input()
        {
            if(_pageType1 == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log($"Hiding {_pageType0.ToString()}, then Showing {_pageType1.ToString()}");
                _persistentUIManager.HidePage(_pageType0, _pageType1);
            }
            if(Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log($"Hiding {_pageType0.ToString()}, Wait, then Showing {_pageType1.ToString()}");
                _persistentUIManager.HidePage(_pageType0, _pageType1, true);
            }
            if(Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log($"Hiding {_pageType1.ToString()}");
                _persistentUIManager.HidePage(_pageType1);
            }
        }
#endif
    }
}
