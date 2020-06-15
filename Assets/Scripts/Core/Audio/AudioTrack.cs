using System;
using UnityEngine;

namespace Ukiyo.Unity.Core.Audio
{
    [Serializable]
    public class AudioTrack
    {
        [SerializeField] AudioSource source;
        [SerializeField] AudioObject[] audios;

        public AudioSource Source => source;
        public AudioObject[] Audios => audios;
    }
}