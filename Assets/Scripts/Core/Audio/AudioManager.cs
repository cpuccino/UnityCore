using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Utilities;
using UnityEngine;

namespace UnityCore.Audio
{
    public class AudioManager : SingletonBehaviour<AudioManager>
    {
        [NaughtyAttributes.ReorderableList] [SerializeField] private AudioTrack[] _tracks = default;
        private Dictionary<AudioType, AudioTrack> _tracksMap;
        private Dictionary<AudioType, IEnumerator> _taskQueueMap;

        private AudioManager()
        {
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _tracksMap = new Dictionary<AudioType, AudioTrack>();
            _taskQueueMap = new Dictionary<AudioType, IEnumerator>();
            if (_tracks == null) _tracks = new AudioTrack[0];

            RegisterTracks();
        }

        private void RegisterTracks()
        {
            foreach (var track in _tracks)
            {
                RegisterTrack(track);
            }
        }

        private void RegisterTrack(AudioTrack track)
        {
            foreach (var audio in track.Audios)
            {
                if (_tracksMap.ContainsKey(audio.Type))
                {
                    Debug.LogWarning($"Audio [{audio.Type.ToString()}] has already been registered");
                    return;
                }

                _tracksMap.Add(audio.Type, track);
                Debug.Log($"Audio [{audio.Type.ToString()}] has been successfully registered");
            }
        }

        private AudioTrack GetAudioTrack(AudioType type, string operation = "ACCESS")
        {
            if (!_tracksMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on an audio track [{type.ToString()}] that has not been registered");
                return null;
            }

            return _tracksMap[type];
        }

        private IEnumerator GetQueuedTask(AudioType type, string operation = "ACCESS")
        {
            if (!_taskQueueMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on an queued task [{type.ToString()}] that doesn't exist");
                return null;
            }

            return _taskQueueMap[type];
        }

        private AudioClip GetAudioClipFromTrack(AudioType type, AudioTrack track, string operation = "ACCESS")
        {
            foreach (var audioObject in track.Audios)
            {
                if (audioObject.Type != type) continue;
                return audioObject.Clip;
            }

            return null;
        }

        private void AddAudioTask(AudioTask task)
        {
            var audioTrack = GetAudioTrack(task.Type, "ADD_AUDIO_TASK");
            if (audioTrack == null) return;

            RemoveConflictingTaskInQueue(task.Type);

            var taskRunner = RunAudioTask(task);
            _taskQueueMap.Add(task.Type, taskRunner);

            StartCoroutine(taskRunner);

            Debug.Log($"Audio task has been created for [{task.Type.ToString()}]: {task.Action.ToString()}");
        }

        private void RemoveConflictingTaskInQueue(AudioType type)
        {
            if (_taskQueueMap.ContainsKey(type)) RemoveAudioTask(type);

            var taskQueueMapKeys = _taskQueueMap.Keys.ToArray();

            foreach (var audioType in taskQueueMapKeys)
            {
                if (audioType == AudioType.None) continue;

                var taskExistingAudioTrack = GetAudioTrack(audioType, "REMOVE_CONFLICTING_AUDIO_TASK");
                var taskInsertAudioTrack = GetAudioTrack(type, "REMOVE_CONFLICTING_AUDIO_TASK");

                if (taskInsertAudioTrack == null || taskExistingAudioTrack == null) continue;
                if (taskExistingAudioTrack.Source != taskInsertAudioTrack.Source) continue;

                RemoveAudioTask(audioType);
            }
        }

        private void RemoveAudioTask(AudioType type)
        {
            var audioTask = GetQueuedTask(type, "REMOVE_AUDIO_TASK");
            if (audioTask == null) return;

            StopCoroutine(audioTask);
            _taskQueueMap.Remove(type);
        }

        private IEnumerator RunAudioTask(AudioTask task)
        {
            yield return new WaitForSeconds(task.Options.Delay);

            var audioTrack = GetAudioTrack(task.Type, "RUN_AUDIO_TASK");
            audioTrack.Source.clip = GetAudioClipFromTrack(task.Type, audioTrack, "RUN_AUDIO_TASK");

            HandleAudioTask(audioTrack, task);

            var fadeFromStart = task.Action == AudioTaskAction.Start || task.Action == AudioTaskAction.Restart;
            StartCoroutine(FadeTrackVolume(audioTrack, fadeFromStart, task.Options.Duration, () =>
                {
                    if (task.Action == AudioTaskAction.Stop) audioTrack.Source.Stop();

                    var queuedTasks = _taskQueueMap.ToArray().Aggregate("", (acc, curr) => acc + $"[{curr.Key.ToString()}] ");
                    _taskQueueMap.Remove(task.Type);
                    if (_taskQueueMap.Count > 0) Debug.Log($"Task: {queuedTasks}");
                    Debug.Log($"Queued tasks: {_taskQueueMap.Count}");
                }
            ));
        }

        private void HandleAudioTask(AudioTrack audioTrack, AudioTask task)
        {
            switch (task.Action)
            {
                case AudioTaskAction.Start:
                    audioTrack.Source.Play();
                    break;

                case AudioTaskAction.Stop:
                    if (task.Options.Duration < Double.Epsilon)
                        audioTrack.Source.Stop();
                    break;

                case AudioTaskAction.Restart:
                    audioTrack.Source.Stop();
                    audioTrack.Source.Play();
                    break;

                default:
                    break;
            }
        }

        private IEnumerator FadeTrackVolume(AudioTrack audioTrack, bool start = false, float duration = 0, Action callback = null)
        {
            if (duration > Double.Epsilon)
            {
                float initial = start ? 0 : 1;
                float target = start ? 1 : 0;
                float timer = 0;

                while (timer <= duration)
                {
                    audioTrack.Source.volume = Mathf.Lerp(initial, target, timer / duration);
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            if (callback != null) callback();
        }

        private void OnDisable()
        {
            if (_taskQueueMap == null) return;
            foreach (var task in _taskQueueMap)
            {
                StopCoroutine(task.Value);
            }
        }

        public void PlayAudio(AudioType type, AudioTaskOptions? options = null)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Start, type, options));
        }

        public void StopAudio(AudioType type, AudioTaskOptions? options = null)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Stop, type, options));
        }

        public void RestartAudio(AudioType type, AudioTaskOptions? options = null)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Restart, type, options));
        }
    }
}