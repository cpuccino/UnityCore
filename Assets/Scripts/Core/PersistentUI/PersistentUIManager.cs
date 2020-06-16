using System.Collections;
using System.Collections.Generic;
using UnityCore.Utilities;
using UnityEngine;

namespace UnityCore.PersistentUI
{
    public class PersistentUIManager : SingletonBehaviour<PersistentUIManager>
    {
        private Dictionary<PersistentUIType, PersistentUI> _persistentUIMap;
        [SerializeField] private PersistentUIType _initialPersistentUI = default;
        [NaughtyAttributes.ReorderableList] [SerializeField] private PersistentUI[] _persistentUIList = default;

        private PersistentUIManager()
        {
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _persistentUIMap = new Dictionary<PersistentUIType, PersistentUI>();
            if (_persistentUIList == null) _persistentUIList = new PersistentUI[0];

            RegisterPersistentUIList();

            if (_initialPersistentUI != PersistentUIType.None)
            {
                ShowPersistentUI(_initialPersistentUI);
            }
        }

        public void ShowPersistentUI(PersistentUIType persistentUIToShowType)
        {
            if (persistentUIToShowType == PersistentUIType.None) return;
            var persistentUI = GetPersistentUI(persistentUIToShowType, "SHOW_PERSISTENT_UI");
            if (persistentUI == null) return;

            persistentUI.gameObject.SetActive(true);
            persistentUI.Animate(true);
        }

        public void HidePersistentUI(PersistentUIType persistentUIToHideType, PersistentUIType persistentUIToShowType = PersistentUIType.None, bool waitForExit = false)
        {
            if (persistentUIToHideType == PersistentUIType.None) return;

            var persistentUIToHide = GetPersistentUI(persistentUIToHideType, "HIDE_PERSISTENT_UI");
            if (persistentUIToHide == null) return;

            if (persistentUIToHide.gameObject.activeSelf)
            {
                persistentUIToHide.Animate(false);
            }

            if (waitForExit && persistentUIToHide.UseAnimation)
            {
                var persistentUIToShow = GetPersistentUI(persistentUIToShowType, "SHOW_PERSISTENT_UI");
                StopCoroutine(WaitForPersistentUIExit(persistentUIToHide, persistentUIToShow));
                StartCoroutine(WaitForPersistentUIExit(persistentUIToHide, persistentUIToShow));
            }
            else
            {
                ShowPersistentUI(persistentUIToShowType);
            }
        }

        private IEnumerator WaitForPersistentUIExit(PersistentUI persistentUIToHide, PersistentUI persistentUIToShow)
        {
            yield return null;
            while (persistentUIToHide.AnimationState != PersistentUIAnimationState.None)
            {
                yield return null;
            }

            ShowPersistentUI(persistentUIToShow.Type);
        }

        private void RegisterPersistentUIList()
        {
            foreach (var persistentUI in _persistentUIList)
            {
                persistentUI.gameObject.SetActive(false);
                RegisterPersistentUI(persistentUI);
            }
        }

        private void RegisterPersistentUI(PersistentUI persistentUI)
        {
            if (_persistentUIMap.ContainsKey(persistentUI.Type))
            {
                Debug.LogWarning($"Persistent UI [{persistentUI.Type.ToString()}] has already been registered");
                return;
            }

            _persistentUIMap.Add(persistentUI.Type, persistentUI);
            Debug.Log($"Persistent UI [{persistentUI.Type.ToString()}] has been successfully registered");
        }

        private PersistentUI GetPersistentUI(PersistentUIType type, string operation = "ACCESS")
        {
            if (!_persistentUIMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on a Persistent UI [{type.ToString()}] that has not been registered");
                return null;
            }

            return _persistentUIMap[type];
        }

        public bool IsPersistentUIActive(PersistentUIType type)
        {
            var persistentUI = GetPersistentUI(type);
            if (persistentUI == null) return false;

            return _persistentUIMap[type].Active;
        }
    }
}