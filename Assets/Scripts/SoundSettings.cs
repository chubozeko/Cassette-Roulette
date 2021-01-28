using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider masterVolume;
    public bool playMusic = true;
    public AudioSource normalPlayer;
    public AudioSource rewindPlayer;

    // Sound Settings
    public void SoundToggleButton()
    {
        FindObjectOfType<AudioManager>().soundEffectAudio.mute = !FindObjectOfType<AudioManager>().soundEffectAudio.mute;
        if (FindObjectOfType<AudioManager>().soundEffectAudio.mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
    }

    public void MusicToggleButton()
    {
        playMusic = !playMusic;
        if (playMusic)
        {
            PlayerPrefs.SetInt("Music", 1);
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().menuMusic;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (SceneManager.GetActiveScene().name == "Game_0")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
                // FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
            }

        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
        }
    }

    public void AdjustVolume()
    {
        FindObjectOfType<AudioManager>().soundEffectAudio.volume = masterVolume.value;
        normalPlayer.volume = masterVolume.value;
        rewindPlayer.volume = masterVolume.value;
    }

}
