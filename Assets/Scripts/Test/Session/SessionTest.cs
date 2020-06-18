using UnityEngine;

namespace UnityCore.Session
{
    // Replace with automated tests
    public class SessionTest : MonoBehaviour
    {
#if UNITY_EDITOR

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!CoreSystem.SessionManager.Paused) CoreSystem.SessionManager.PauseSession();
                else CoreSystem.SessionManager.ResumeSession();
            }
        }

#endif
    }
}