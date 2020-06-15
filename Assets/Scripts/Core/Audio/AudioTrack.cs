namespace Ukiyo.Unity.Core.Audio
{
    public enum AudioTaskAction
    {
        Start, Stop, Restart
    }

    public class AudioTask
    {
        public AudioTaskAction Action { get; }
        public AudioType Type { get; }

        public AudioTask(AudioTaskAction action, AudioType type)
        {
            Action = action;
            Type = type;
        }
    } 
}