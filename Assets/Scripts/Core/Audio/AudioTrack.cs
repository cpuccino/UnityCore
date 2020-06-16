using UnityEngine;

namespace Ukiyo.Unity.Core.Audio
{
    public class AudioTrack: MonoBehaviour
    {
        [NaughtyAttributes.ReorderableList][SerializeField] AudioObject[] audios;

        public AudioSource Source { get; private set; }
        public AudioObject[] Audios => audios;

        void Awake()
        {
            Source = GetComponent<AudioSource>();
        }
    }
}