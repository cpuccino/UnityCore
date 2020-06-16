using UnityEngine;

namespace UnityCore.Audio
{
    public class AudioTrack: MonoBehaviour
    {
        [NaughtyAttributes.ReorderableList][SerializeField] AudioObject[] audios = default;

        public AudioSource Source { get; private set; }
        public AudioObject[] Audios => audios;

        void Awake()
        {
            Source = GetComponent<AudioSource>();
        }
    }
}