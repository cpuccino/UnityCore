using System;
using UnityCore.Utilities;

namespace UnityCore.Session
{
    public class SessionManager : SingletonBehaviour<SessionManager>
    {
        public long SessionStartTime { get; private set; }
        public bool Paused { get; private set; }

        public event Action OnSessionPaused;

        public event Action OnSessionResumed;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            SessionStartTime = EpochSeconds();
            Paused = false;
        }

        private long EpochSeconds()
        {
            var epoch = new DateTimeOffset(DateTime.UtcNow);
            return epoch.ToUnixTimeSeconds();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus) PauseSession();
        }

        public void PauseSession()
        {
            Paused = true;
            if (OnSessionPaused == null) return;

            OnSessionPaused();
        }

        public void ResumeSession()
        {
            Paused = false;
            if (OnSessionResumed == null) return;

            OnSessionResumed();
        }
    }
}