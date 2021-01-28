using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle soundToggle;
    public Toggle musicToggle;
    public Button[] levels;

    private void Awake()
    {
        // SOUND (mute)
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = false;
        }
        soundToggle.isOn = !FindObjectOfType<AudioManager>().soundEffectAudio.mute;
        // MUSIC
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            // PlayerPrefs.SetInt("Music", 1);
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
            musicToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
            musicToggle.isOn = false;
        }
        
    }

    public void LoadLevelPanel()
    {
        levels[0].interactable = true;
        PlayerPrefs.SetInt("Level_1_Unlocked", 1);

        for (int i=1; i<10; i++)
        {
            string levelStr = "Level_" + (i + 1) + "_Unlocked";
            if ((PlayerPrefs.GetInt(levelStr) != 1) && (PlayerPrefs.GetInt(levelStr) != 0))
            {
                if (i == 0)
                {
                    PlayerPrefs.SetInt(levelStr, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(levelStr, 0);
                }
            }
            else if (PlayerPrefs.GetInt(levelStr) == 1)
            {
                levels[i].interactable = true;
            }
            else if (PlayerPrefs.GetInt(levelStr) == 0)
            {
                levels[i].interactable = false;
            } 
        }
    }
    public void LoadTimedGame(int difficulty)
    {
        switch(difficulty)
        {
            case 0: // EASY: 6 mins
                PlayerPrefs.SetFloat("Time", 360f);
                PlayerPrefs.SetInt("IsTimeCountdown", 1);
                PlayerPrefs.SetInt("NumberOfGOLD", -1);
                PlayerPrefs.SetString("Objective", "Time Trial: Collect as many CDs within 6 minutes!");
                break;
            case 1: // INTERMEDIATE: 4 mins
                PlayerPrefs.SetFloat("Time", 240f);
                PlayerPrefs.SetInt("IsTimeCountdown", 1);
                PlayerPrefs.SetInt("NumberOfGOLD", -1);
                PlayerPrefs.SetString("Objective", "Time Trial: Collect as many CDs within 4 minutes!");
                break;
            case 2: // HARD: 2 mins
                PlayerPrefs.SetFloat("Time", 120f);
                PlayerPrefs.SetInt("IsTimeCountdown", 1);
                PlayerPrefs.SetInt("NumberOfGOLD", -1);
                PlayerPrefs.SetString("Objective", "Time Trial: Collect as many CDs within 2 minutes!");
                break;
            case 3: // INSANE: 1 min
                PlayerPrefs.SetFloat("Time", 60f);
                PlayerPrefs.SetInt("IsTimeCountdown", 1);
                PlayerPrefs.SetInt("NumberOfGOLD", -1);
                PlayerPrefs.SetString("Objective", "Time Trial: Collect as many CDs within 1 minute!");
                break;
            default:
                break;
        }
        SceneManager.LoadScene("Game_0");
    }

    public void LoadLevel(int level)
    {
        switch (level)
        {
            case 1: // LEVEL 1
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 2);
                PlayerPrefs.SetString("Objective", "Level 1: Collect 2 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_1_Unlocked", 1);
                PlayerPrefs.SetInt("Level", 1);
                break;
            case 2: // LEVEL 2
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 4);
                PlayerPrefs.SetString("Objective", "Level 2: Collect 4 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_2_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 2);
                break;
            case 3: // LEVEL 3
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 6);
                PlayerPrefs.SetString("Objective", "Level 3: Collect 6 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_3_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 3);
                break;
            case 4: // LEVEL 4
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 8);
                PlayerPrefs.SetString("Objective", "Level 4: Collect 8 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_4_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 4);
                break;
            case 5: // LEVEL 5
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 10);
                PlayerPrefs.SetString("Objective", "Level 5: Collect 10 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_5_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 5);
                break;
            case 6: // LEVEL 6
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 12);
                PlayerPrefs.SetString("Objective", "Level 6: Collect 12 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_6_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 6);
                break;
            case 7: // LEVEL 7
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 14);
                PlayerPrefs.SetString("Objective", "Level 7: Collect 14 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_7_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 7);
                break;
            case 8: // LEVEL 8
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 16);
                PlayerPrefs.SetString("Objective", "Level 8: Collect 16 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_8_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 8);
                break;
            case 9: // LEVEL 9 
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 18);
                PlayerPrefs.SetString("Objective", "Level 9: Collect 18 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_9_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 9);
                break;
            case 10: // LEVEL 10
                PlayerPrefs.SetFloat("Time", 0f);
                PlayerPrefs.SetInt("IsTimeCountdown", 0);
                PlayerPrefs.SetInt("NumberOfGOLD", 20);
                PlayerPrefs.SetString("Objective", "Level 10: Collect 20 CDs to pass the level!");
                // PlayerPrefs.SetInt("Level_10_Unlocked", 0);
                PlayerPrefs.SetInt("Level", 10);
                break;
            default:
                break;
        }
        SceneManager.LoadScene("Game_0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
