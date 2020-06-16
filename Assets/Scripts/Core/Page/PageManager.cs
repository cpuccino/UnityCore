using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Utilities;

namespace UnityCore.Page
{
    public class PersistentUIManager : Singleton<PersistentUIManager>
    {
        Dictionary<PageType, Page> _pageMap;
        [SerializeField] PageType _initialPage = default;
        [NaughtyAttributes.ReorderableList][SerializeField] Page[] _pages = default;

        private PersistentUIManager() {}
        
        void Awake()
        {
            Initialize();
        }
        
        void Initialize()
        {
            _pageMap = new Dictionary<PageType, Page>();
            if(_pages == null) _pages = new Page[0];
            
            RegisterPages();

            if(_initialPage != PageType.None)
            {
                ShowPage(_initialPage);
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
            foreach(var page in _pages)
            {
                page.gameObject.SetActive(false);
                RegisterPage(page);
            }
        }

        void RegisterPage(Page page)
        {
            if(_pageMap.ContainsKey(page.Type))
            {
                Debug.LogWarning($"Page [{page.Type.ToString()}] has already been registered");
                return;
            }

            _pageMap.Add(page.Type, page);
            Debug.Log($"Page [{page.Type.ToString()}] has been successfully registered");
        }

        Page GetPage(PageType type, string operation = "ACCESS")
        {
            if(!_pageMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on a page [{type.ToString()}] that has not been registered");
                return null;
            }

            return _pageMap[type];
        }

        public bool IsPageActive(PageType type)
        {
            var page = GetPage(type);
            if(page == null) return false;

            return _pageMap[type].Active;
        }
    }
}