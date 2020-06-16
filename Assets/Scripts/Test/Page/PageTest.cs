﻿using System;
using System.Linq;
using UnityEngine;

namespace UnityCore.Page
{
    // Replace with automated tests
    public class PageTest : MonoBehaviour
    {
        [SerializeField] PageManager pageManager = default;

        [SerializeField] PageType pageType0 = default;
        [SerializeField] PageType pageType1 = default;

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
                    pageManager.HidePage(pageType);
                }
            }
        }

        void HandlePage0Input()
        {
            if(pageType0 == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log($"Showing {pageType0.ToString()}");
                pageManager.ShowPage(pageType0);
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log($"Hiding {pageType0.ToString()}");
                pageManager.HidePage(pageType0);
            }
        }

        void HandlePage1Input()
        {
            if(pageType1 == PageType.None) return;

            if(Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log($"Hiding {pageType0.ToString()}, then Showing {pageType1.ToString()}");
                pageManager.HidePage(pageType0, pageType1);
            }
            if(Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log($"Hiding {pageType0.ToString()}, Wait, then Showing {pageType1.ToString()}");
                pageManager.HidePage(pageType0, pageType1, true);
            }
            if(Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log($"Hiding {pageType1.ToString()}");
                pageManager.HidePage(pageType1);
            }
        }
#endif
    }
}
