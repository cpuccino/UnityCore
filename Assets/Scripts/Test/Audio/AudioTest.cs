using UnityEngine;

namespace UnityCore.Audio
{
    // Replace with automated tests
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] AudioManager audioManager = default;

        [SerializeField] AudioType audioType0 = default;
        [SerializeField] AudioType audioType1 = default;

        [SerializeField] float delay = default;
        [SerializeField] float fadeDuration = default;

#if UNITY_EDITOR
        void Update()
        {
            HandleAudio0Input();
            HandleAudio1Input();
        }
        
        void HandleAudio0Input()
        {
            var audioTaskOptions = new AudioTaskOptions();
            audioTaskOptions.Delay = delay;
            audioTaskOptions.Duration = fadeDuration;
            
            if(Input.GetKeyUp(KeyCode.A))
            {
                audioManager.PlayAudio(audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                audioManager.StopAudio(audioType0, audioTaskOptions);
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                audioManager.RestartAudio(audioType0);
            }
        }

        void HandleAudio1Input()
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                audioManager.PlayAudio(audioType1);
            }
            if(Input.GetKeyUp(KeyCode.G))
            {
                audioManager.StopAudio(audioType1);
            }
            if(Input.GetKeyUp(KeyCode.H))
            {
                audioManager.RestartAudio(audioType1);
            }
        }
#endif
    }
}
