using System;
using UnityEngine;

namespace UnityCore.Audio
{
    [Serializable]
    public class AudioObject
    {
        [SerializeField] private AudioType _type = default;
        [SerializeField] private AudioClip _clip = default;

        public AudioType Type => _type;
        public AudioClip Clip => _clip;
    }
}