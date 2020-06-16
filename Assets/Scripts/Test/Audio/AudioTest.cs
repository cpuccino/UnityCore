using UnityEngine;

namespace UnityCore.Audio
{
    // Replace with automated tests
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] AudioManager _audioManager = default;

        [SerializeField] AudioType _audioType0 = default;
        [SerializeField] AudioType _audioType1 = default;

        [SerializeField] float _delay = default;
        [SerializeField] float _fadeDuration = default;

#if UNITY_EDITOR
        void Update()
        {
            HandleAudio0Input();
            HandleAudio1Input();
        }
        
        void HandleAudio0Input()
        {
            var audioTaskOptions = new AudioTaskOptions();
            audioTaskOptions.Delay = _delay;
            audioTaskOptions.Duration = _fadeDuration;
            
            if(Input.GetKeyUp(KeyCode.A))
            {
                _audioManager.PlayAudio(_audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                _audioManager.StopAudio(_audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                _audioManager.RestartAudio(_audioType0);
            }
        }

        void HandleAudio1Input()
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                _audioManager.PlayAudio(_audioType1);
            }
            if(Input.GetKeyUp(KeyCode.G))
            {
                _audioManager.StopAudio(_audioType1);
            }
            if(Input.GetKeyUp(KeyCode.H))
            {
                _audioManager.RestartAudio(_audioType1);
            }
        }
#endif
    }
}
