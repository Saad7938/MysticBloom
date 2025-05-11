using UnityEngine;

namespace Farm.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("----------- Audio Source -----------")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource SFXSource;

        [Header("----------- Audio Clip -----------")]
        public AudioClip background;
        public AudioClip Harvesting;
        public AudioClip Watering;
        public AudioClip Coins_Gained;

        void Start()
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
        public void playSFX(AudioClip clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

}
