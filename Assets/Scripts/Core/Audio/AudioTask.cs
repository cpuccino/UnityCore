namespace UnityCore.Audio
{
    public enum AudioTaskAction
    {
        Start, Stop, Restart
    }

    public struct AudioTaskOptions
    {
        public float Duration { get; set; }
        public float Delay { get; set; }
    }

    public class AudioTask
    {
        public AudioTaskAction Action { get; }
        public AudioType Type { get; }
        public AudioTaskOptions Options { get; set; }

        public AudioTask(AudioTaskAction action, AudioType type, AudioTaskOptions? options)
        {
            Action = action;
            Type = type;

            Options = options ?? new AudioTaskOptions();
        }
    }
}