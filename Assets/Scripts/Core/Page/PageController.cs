using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Utilities;

namespace UnityCore.Page
{
    public class PageController : Singleton<PageController>
    {
        Dictionary<PageType, Page> pageMap;
        [SerializeField] PageType initialPage = default;
        [NaughtyAttributes.ReorderableList][SerializeField] Page[] pages = default;

        private PageController() {}
        
        void Awake()
        {
            Initialize();
        }
        
        void Initialize()
        {
            pageMap = new Dictionary<PageType, Page>();
            if(pages == null) pages = new Page[0];
            
            RegisterPages();

            if(initialPage != PageType.None)
            {
                ShowPage(initialPage);
            }
        }
        
        public void ShowPage(PageType pageToShowType)
        {
            if(pageToShowType == PageType.None) return;
            var page = GetPage(pageToShowType, "SHOW_PAGE");
            if(page == null) return;
            
            page.gameObject.SetActive(true);
            page.Animate(true);
        }

        public void HidePage(PageType pageToHideType, PageType pageToShowType = PageType.None, bool waitForExit = false)
        {
            if(pageToHideType == PageType.None) return;

            var pageToHide = GetPage(pageToHideType, "HIDE_PAGE");
            if(pageToHide == null) return;

            if(pageToHide.gameObject.activeSelf)
            {
                pageToHide.Animate(false);
            }

            if(waitForExit && pageToHide.UseAnimation)
            {
                var pageToShow = GetPage(pageToShowType, "SHOW_PAGE");
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
            foreach(var page in pages)
            {
                page.gameObject.SetActive(false);
                RegisterPage(page);
            }
        }

        void RegisterPage(Page page)
        {
            if(pageMap.ContainsKey(page.Type))
            {
                Debug.LogWarning($"Page [{page.Type.ToString()}] has already been registered");
                return;
            }

            pageMap.Add(page.Type, page);
            Debug.Log($"Page [{page.Type.ToString()}] has been successfully registered");
        }

        Page GetPage(PageType type, string operation = "ACCESS")
        {
            if(!pageMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on a page [{type.ToString()}] that has not been registered");
                return null;
            }

            return pageMap[type];
        }

        public bool IsPageActive(PageType type)
        {
            var page = GetPage(type);
            if(page == null) return false;

            return pageMap[type].Active;
        }
    }
}