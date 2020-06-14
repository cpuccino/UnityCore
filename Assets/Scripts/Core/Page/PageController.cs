using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ukiyo.Unity.Core.Page
{
    public class PageController : MonoBehaviour
    {
        static PageController instance;

        Dictionary<PageType, Page> PageMap;

        [SerializeField] PageType initialPage;

        [SerializeField] Page[] pages;

        void Awake()
        {
            InitializeInstance();
        }

        void InitializeInstance()
        {
            if(instance == null)
            {
                instance = this;
                PageMap = new Dictionary<PageType, Page>();
                RegisterPages();

                if(initialPage != PageType.None)
                {
                    ShowPage(initialPage);
                }
            }
        }
        
        public void ShowPage(PageType pageToShowType)
        {
            if(pageToShowType == PageType.None) return;
            if(!PageMap.ContainsKey(pageToShowType))
            {
                Debug.LogWarning($"You are trying to show a page [{pageToShowType.ToString()}] that has not been registered");
                return;
            }

            Page page = GetPage(pageToShowType);
            page.gameObject.SetActive(true);
            page.Animate(true);
        }

        public void HidePage(PageType pageToHideType, PageType pageToShowType = PageType.None, bool waitForExit = false)
        {
            if(pageToHideType == PageType.None) return;
            if(!PageMap.ContainsKey(pageToHideType))
            {
                Debug.LogWarning($"You are trying to hide a page [{pageToHideType.ToString()}] that has not been registered");
                return; 
            }

            Page pageToHide = GetPage(pageToHideType);
            
            if(pageToHide.gameObject.activeSelf)
            {
                pageToHide.Animate(false);
            }

            if(waitForExit && pageToHide.UseAnimation)
            {
                Page pageToShow = GetPage(pageToShowType);
                StopCoroutine(WaitForPageExit(pageToHide, pageToShow));
                StartCoroutine(WaitForPageExit(pageToHide, pageToShow));
            }
            else
            {
                ShowPage(pageToShowType);
            }
        }
        
        IEnumerator WaitForPageExit(Page pageToHide, Page pageToShow)
        {
            yield return null;
            while(pageToHide.AnimationState != PageAnimationState.None)
            {
                yield return null;
            }

            ShowPage(pageToShow.Type);
        }

        void RegisterPages()
        {
            foreach(Page page in pages)
            {
                page.gameObject.SetActive(false);
                RegisterPage(page);
            }
        }

        void RegisterPage(Page page)
        {
            if(!PageMap.ContainsKey(page.Type))
            {
                PageMap.Add(page.Type, page);
                Debug.Log($"Page [{page.Type.ToString()}] has been successfully registered");
                return;
            }

            Debug.LogWarning($"Page [{page.Type.ToString()}] has already been registered");
        }

        Page GetPage(PageType type)
        {
            if(!PageMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to get a page [{type.ToString()}] that has not been registered");
                return null;
            }

            return PageMap[type];
        }

        public bool IsPageActive(PageType type)
        {
            if(!PageMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to detect if a page is active [{type.ToString()}], but it's not registered");
                return false;
            }

            return PageMap[type].Active;
        }
    }
}