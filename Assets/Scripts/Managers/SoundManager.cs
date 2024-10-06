using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Mangers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [SerializeField] private AudioSource _soundFx;
        [SerializeField] private AudioClip _landClip;
        [SerializeField] private AudioClip _deathClip;
        [SerializeField] private AudioClip _iceBreakClip;
        [SerializeField] private AudioClip _gameOverClip;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        internal void LandSound()
        {
            _soundFx.clip = _landClip;
            _soundFx.Play();
        }

        internal void IceBreakClip()
        {
            _soundFx.clip = _iceBreakClip;
            _soundFx.Play();
        }

        internal void DeathSound()
        {
            _soundFx.clip = _deathClip;
            _soundFx.Play();
        }

        internal void GameOverSound()
        {
            _soundFx.clip = _gameOverClip;
            _soundFx.Play();
        }
    }
}
