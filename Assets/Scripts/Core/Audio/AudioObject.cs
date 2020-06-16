using System;
using UnityEngine;

namespace UnityCore.Audio
{
    [Serializable]
    public class AudioObject
    {
        [SerializeField] AudioType _type = default;
        [SerializeField] AudioClip _clip = default;

        public AudioType Type => _type;
        public AudioClip Clip => _clip;
    }
}
