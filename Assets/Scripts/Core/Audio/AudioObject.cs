using System;
using UnityEngine;

namespace UnityCore.Audio
{
    [Serializable]
    public class AudioObject
    {
        [SerializeField] AudioType type = default;
        [SerializeField] AudioClip clip = default;

        public AudioType Type => type;
        public AudioClip Clip => clip;
    }
}
