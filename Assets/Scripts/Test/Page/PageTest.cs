using System;
using System.Linq;
using Ukiyo.Unity.Core.Page;
using UnityEngine;

// Replace with automated tests
public class PageTest : MonoBehaviour
{
    [SerializeField] bool __enable;

    bool enable;

    public PageController pageController;

    [SerializeField] Page page0;

    [SerializeField] Page page1;

    #if UNITY_EDITOR
    void Awake()
    {
        enable = __enable;
        if(!enable) return;
        
        Debug.Log("Page Test Attached");
    }

    void Update()
    {
        if(!enable) return;

        if(Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("Hiding All Pages");
            PageType[] pageTypes = Enum.GetValues(typeof(PageType)).Cast<PageType>().ToArray();
            foreach (PageType pageType in pageTypes)
            {
                pageController.HidePage(pageType);
            }
        }

        if(page0 == null || page0.Type == PageType.None) return;
        string page0Name = Enum.GetName(typeof(PageType), page0.Type);

        if(Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log($"Show {page0Name}");
            pageController.ShowPage(page0.Type);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log($"Hide {page0Name}");
            pageController.HidePage(page0.Type);
        }

        if(page1 == null || page1.Type == PageType.None) return;
        string page1Name = Enum.GetName(typeof(PageType), page1.Type);

        if(Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log($"Hide {page0Name}, Show {page1Name}");
            pageController.HidePage(page0.Type, page1.Type);
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log($"Hide {page0Name}, Wait, then Show {page1Name}");
            pageController.HidePage(page0.Type, page1.Type, true);
        }
        if(Input.GetKeyUp(KeyCode.T))
        {
            Debug.Log($"Hide {page1Name}");
            pageController.HidePage(page1.Type);
        }
    }
    #endif
}
