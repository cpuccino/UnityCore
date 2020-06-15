﻿using UnityEngine;

namespace Ukiyo.Unity.Core.Audio
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] bool __enable;
        bool enable;

        [SerializeField] AudioController audioController;

        [SerializeField] AudioType audioType0;
        [SerializeField] AudioType audioType1;

        #if UNITY_EDITOR
        void Awake()
        {
            enable = __enable;
            if(!enable) return;

            Debug.Log("Audio Test Attached");
        }

        void Update()
        {
            if(!enable) return;
            
            if(Input.GetKeyUp(KeyCode.A))
            {
                audioController.PlayAudio(audioType0);
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                audioController.StopAudio(audioType0);
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                audioController.RestartAudio(audioType0);
            }

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
