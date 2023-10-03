using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundInvoker : MonoBehaviour
    {
        [SerializeField] private float volume = 1f;

        private AudioSource _audioSource;
        public LevelSelector CurrentLevelSelector { get; set; }

        private void Awake()
        {
            _audioSource ??= GetComponent<AudioSource>();
        }

        public void Initialize(LevelSelector levelSelector)
        {
            CurrentLevelSelector = levelSelector;
        }

        public void InvokeClip()
        {
            PlayOneShot(CurrentLevelSelector.GameConfig.DifferenceIsFoundClip);
        }
        
        private void PlayOneShot(AudioClip clip)
        {
            _audioSource.volume = volume;
            _audioSource.clip = clip;
            _audioSource.PlayOneShot(clip);
        }
    }
}