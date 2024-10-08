using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Audio
{
    // Enum for multiple SFX types
    public enum SFXType
    {
        Dropping,
        Collecting,
        Bombing
    }

    [System.Serializable]
    public struct SFXAudio
    {
        public SFXType sfxType;
        public AudioClip audioClip;
    }

    [CreateAssetMenu(fileName = "AudioSO", menuName = "Audio/AudioSO")]
    public class AudioSO : ScriptableObject
    {
        [Header("Background Music")]
        [SerializeField] public AudioClip backgroundMusicClip;
        [SerializeField] [Range(0f, 1f)] public float backgroundMusicVolume = 0.5f; 

        [Header("SFX")]
        [SerializeField] public SFXAudio[] sfxClips; 
        [SerializeField] [Range(0f, 1f)] public float sfxVolume = 0.5f; 

        // Method to get SFX clip by enum
        public AudioClip GetSFXClip(SFXType sfxType)
        {
            foreach (var sfx in sfxClips)
            {
                if (sfx.sfxType == sfxType)
                {
                    return sfx.audioClip; // Return the audioClip
                }
            }

            Debug.LogWarning($"SFX clip for {sfxType} not found. Returning null.");
            return null;
        }
    }
}
