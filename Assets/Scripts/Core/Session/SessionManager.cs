using System;
using UnityCore.Utilities;
using UnityEngine;

namespace UnityCore.Session
{
    public class SessionManager : SingletonBehaviour<SessionManager>
    {
        [SerializeField] bool pauseOnLoseFocus = default;

        public long SessionStartTime { get; private set; }
        public bool Paused { get; private set; }

        public event Action OnSessionPaused = default;

        public event Action OnSessionResumed = default;

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
            if (!focus && pauseOnLoseFocus) PauseSession();
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