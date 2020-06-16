using UnityEngine;

namespace UnityCore.Audio
{
    public class AudioTrack : MonoBehaviour
    {
        [NaughtyAttributes.ReorderableList] [SerializeField] private AudioObject[] _audios = default;

        public AudioSource Source { get; private set; }
        public AudioObject[] Audios => _audios;

        private void Awake()
        {
            Source = GetComponent<AudioSource>();
        }
    }
}