using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public AudioClip buttonSound;
    public AudioClip menuMusic;
    public AudioClip gameOverSound;
    public AudioClip getCassetteFromDeck;
    public AudioClip levelComplete;
    public AudioClip mergeCassettes;
    public AudioClip putCassetteInPlayer;
    public AudioClip putCDInStorage;
    public AudioSource soundEffectAudio;
    public int a;  // flag for Game_0
    public int b;  // flag for MainMenu
    public bool vibration;

    void Start()
    {
        a = 0;
        b = 0;
        vibration = true;
        if (Instance == null)
        {
            Instance = this;    // makes sure this is the only AudioManager
        }
        else if (Instance != null)
        {
            Destroy(gameObject);    // if there are others, destroy them
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            soundEffectAudio.clip = menuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }
        
        if (SceneManager.GetActiveScene().name == "Game_0")
        {
            soundEffectAudio.clip = null;
            soundEffectAudio.loop = false;
            // soundEffectAudio.Play(0);
        }

        
        // TOGGLE MUTE
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            soundEffectAudio.mute = true;
        }
        else
        {
            soundEffectAudio.mute = false;
        }

        // DontDestroyOnLoad(gameObject.transform);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game_0" && b == 0)
        {
            soundEffectAudio.clip = null;
            soundEffectAudio.loop = false;
            // soundEffectAudio.Play(0);
            b++;
        }
     
        if (SceneManager.GetActiveScene().name == "MainMenu" && a == 0)
        {
            soundEffectAudio.clip = menuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            a++;
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    public void PlayButtonClick()
    {
        soundEffectAudio.PlayOneShot(buttonSound);
    }
}
