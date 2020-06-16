using UnityEngine;

namespace UnityCore.Audio
{
    public class AudioTrack: MonoBehaviour
    {
        [NaughtyAttributes.ReorderableList][SerializeField] AudioObject[] _audios = default;

        public AudioSource Source { get; private set; }
        public AudioObject[] Audios => _audios;

        void Awake()
        {
            Source = GetComponent<AudioSource>();
        }
    }
}