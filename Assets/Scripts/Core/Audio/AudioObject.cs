using System;
using UnityEngine;

namespace UnityCore.Audio
{
    [Serializable]
    public class AudioObject
    {
        [SerializeField] AudioType type;
        [SerializeField] AudioClip clip;

        public AudioType Type => type;
        public AudioClip Clip => clip;
    }
}
