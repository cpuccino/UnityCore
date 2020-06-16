﻿using UnityEngine;

namespace UnityCore.Audio
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] AudioController audioController = default;

        [SerializeField] AudioType audioType0 = default;
        [SerializeField] AudioType audioType1 = default;

        [SerializeField] float delay = default;
        [SerializeField] float fadeDuration = default;

#if UNITY_EDITOR
        void Update()
        {
            HandleAudio0Input();
            HandleAudio1Input();
        }
        
        void HandleAudio0Input()
        {
            var audioTaskOptions = new AudioTaskOptions();
            audioTaskOptions.Delay = delay;
            audioTaskOptions.Duration = fadeDuration;
            
            if(Input.GetKeyUp(KeyCode.A))
            {
                audioController.PlayAudio(audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                audioController.StopAudio(audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                audioController.RestartAudio(audioType0);
            }
        }

        void HandleAudio1Input()
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                audioController.PlayAudio(audioType1);
            }
            if(Input.GetKeyUp(KeyCode.G))
            {
                audioController.StopAudio(audioType1);
            }
            if(Input.GetKeyUp(KeyCode.H))
            {
                audioController.RestartAudio(audioType1);
            }
        }
#endif
    }
}
