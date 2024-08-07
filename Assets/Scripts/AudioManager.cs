using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    public AudioClip Music;
    public AudioClip dingSound;
    public AudioClip trackSwitch;
    public static float volume;

    void Awake()
    {
        if (PlayerPrefs.HasKey("volumeChangedBool"))
        {
            ChangeMusicVolume();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat("volume");
    }
}
