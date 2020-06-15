using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ukiyo.Unity.Core.Utilities;
using System.Linq;

namespace Ukiyo.Unity.Core.Audio
{
    public class AudioController : Singleton<AudioController>
    {
        [NaughtyAttributes.ReorderableList][SerializeField] AudioTrack[] tracks;
        Dictionary<AudioType, AudioTrack> tracksMap;
        Dictionary<AudioType, IEnumerator> taskQueueMap;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            tracksMap = new Dictionary<AudioType, AudioTrack>();
            taskQueueMap = new Dictionary<AudioType, IEnumerator>();

            RegisterTracks();
        }

        void RegisterTracks()
        {
            foreach(var track in tracks)
            {
                RegisterTrack(track);
            }
        }

        void RegisterTrack(AudioTrack track)
        {
            foreach (var audio in track.Audios)
            {
                if(tracksMap.ContainsKey(audio.Type))
                {
                    Debug.LogWarning($"Audio [{audio.Type.ToString()}] has already been registered");
                    return;
                }

                tracksMap.Add(audio.Type, track);
                Debug.Log($"Audio [{audio.Type.ToString()}] has been successfully registered");
            }
        }

        AudioTrack GetAudioTrack(AudioType type, string operation = "ACCESS")
        {
            if(!tracksMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on an audio track [{type.ToString()}] that has not been registered");
                return null;
            }

            return tracksMap[type];
        }

        IEnumerator GetQueuedTask(AudioType type, string operation = "ACCESS")
        {
            if(!taskQueueMap.ContainsKey(type))
            {
                Debug.LogWarning($"You are trying to perform [{operation}] operation on an queued task [{type.ToString()}] that doesn't exist");
                return null;
            }

            return taskQueueMap[type];
        }

        AudioClip GetAudioClipFromTrack(AudioType type, AudioTrack track, string operation = "ACCESS")
        {
            foreach(var audioObject in track.Audios)
            {
                if(audioObject.Type != type) continue;
                return audioObject.Clip;
            }

            return null;
        }

        void AddAudioTask(AudioTask task)
        {
            var audioTrack = GetAudioTrack(task.Type, "ADD_AUDIO_TASK");
            if(audioTrack == null) return;

            RemoveConflictingTaskInQueue(task.Type);

            var taskRunner = RunAudioTask(task);
            taskQueueMap.Add(task.Type, taskRunner);

            StartCoroutine(taskRunner);

            Debug.Log($"Audio task has been created for [{task.Type.ToString()}]: {task.Action.ToString()}");
        }
        
        void RemoveConflictingTaskInQueue(AudioType type)
        {
            if(taskQueueMap.ContainsKey(type)) RemoveAudioTask(type);

            var taskQueueMapKeys = taskQueueMap.Keys.ToArray();

            foreach(var audioType in taskQueueMapKeys)
            {
                if(audioType == AudioType.None) continue;

                var taskExistingAudioTrack = GetAudioTrack(audioType, "REMOVE_CONFLICTING_AUDIO_TASK");
                var taskInsertAudioTrack = GetAudioTrack(type, "REMOVE_CONFLICTING_AUDIO_TASK");
                
                if(taskInsertAudioTrack == null || taskExistingAudioTrack == null) continue;
                if(taskExistingAudioTrack.Source != taskInsertAudioTrack.Source) continue;

                RemoveAudioTask(audioType);
            }
        }

        void RemoveAudioTask(AudioType type)
        {
            var audioTask = GetQueuedTask(type, "REMOVE_AUDIO_TASK");
            if(audioTask == null) return;

            StopCoroutine(audioTask);
            taskQueueMap.Remove(type);

            // finish current playing animation and transition volume -> fade
            // get audio task, transition volume -> after volume <= 0 -> stopcoroutine
        }

        IEnumerator RunAudioTask(AudioTask task)
        {
            var audioTrack = GetAudioTrack(task.Type, "RUN_AUDIO_TASK");
            audioTrack.Source.clip = GetAudioClipFromTrack(task.Type, audioTrack, "RUN_AUDIO_TASK");

            switch(task.Action)
            {
                case AudioTaskAction.Start: audioTrack.Source.Play();
                    break;
                case AudioTaskAction.Stop:
                    audioTrack.Source.Stop();
                    break;
                case AudioTaskAction.Restart:
                    audioTrack.Source.Stop();
                    audioTrack.Source.Play();
                    break;
                default:
                    break;
            }

            taskQueueMap.Remove(task.Type);
            Debug.Log($"Queued tasks: {taskQueueMap.Count}");

            yield return null;
        }

        void OnDisable()
        {
            foreach(var task in taskQueueMap)
            {
                StopCoroutine(task.Value);
            }
        }

        public void PlayAudio(AudioType type)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Start, type));
        }

        public void StopAudio(AudioType type)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Stop, type));
        }

        public void RestartAudio(AudioType type)
        {
            AddAudioTask(new AudioTask(AudioTaskAction.Restart, type));
        }
    }
}
