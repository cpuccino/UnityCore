using System;
using System.Linq;
using UnityEngine;

namespace UnityCore.PersistentUI
{
    // Replace with automated tests
    public class PersistentUITest : MonoBehaviour
    {
        [SerializeField] private PersistentUIManager _persistentUIManager = default;

        [SerializeField] private PersistentUIType _persistentUIType0 = default;
        [SerializeField] private PersistentUIType _persistentUIType1 = default;

#if UNITY_EDITOR

        private void Update()
        {
            HandlePersistentUIResetInput();
            HandlePersistentUI0Input();
            HandlePersistentUI1Input();
        }

        private void HandlePersistentUIResetInput()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Hiding All Persistent UIs");
                var persistentUITypes = Enum.GetValues(typeof(PersistentUIType)).Cast<PersistentUIType>().ToArray();
                foreach (var persistentUIType in persistentUITypes)
                {
                    _persistentUIManager.HidePersistentUI(persistentUIType);
                }
            }
        }

        private void HandlePersistentUI0Input()
        {
            if (_persistentUIType0 == PersistentUIType.None) return;

            if (Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log($"Showing {_persistentUIType0.ToString()}");
                _persistentUIManager.ShowPersistentUI(_persistentUIType0);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log($"Hiding {_persistentUIType0.ToString()}");
                _persistentUIManager.HidePersistentUI(_persistentUIType0);
            }
        }

        private void HandlePersistentUI1Input()
        {
            if (_persistentUIType1 == PersistentUIType.None) return;

            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log($"Hiding {_persistentUIType0.ToString()}, then Showing {_persistentUIType1.ToString()}");
                _persistentUIManager.HidePersistentUI(_persistentUIType0, _persistentUIType1);
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log($"Hiding {_persistentUIType0.ToString()}, Wait, then Showing {_persistentUIType1.ToString()}");
                _persistentUIManager.HidePersistentUI(_persistentUIType0, _persistentUIType1, true);
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log($"Hiding {_persistentUIType1.ToString()}");
                _persistentUIManager.HidePersistentUI(_persistentUIType1);
            }
        }

#endif
    }
}