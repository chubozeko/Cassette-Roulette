using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("GUI Elements")]
    public Transform gamePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject pausePanel;
    public GameObject deckPosition;
    public GameObject hiddenDeckPosition;
    public Button deckButton;
    public Button rewindButton;
    public GameObject normalPlayer;
    public GameObject rewindPlayer;
    public GameObject resultPlayer;
    public Dropzone hand;
    public int maxHandCapacity = 7;
    public Image Slider_NormalPlayer;
    public Slider normalSlider;
    public Slider rewindSlider;
    public Image Slider_RewindPlayer;
    public Text scoreText;
    public Text timerText;
    public Text GO_GameDetailsText;
    public Text LC_GameDetailsText;
    public Text ObjectiveInfoText;
    public Text goldCollectedText;

    [Header("Cassette Properties")]
    public GameObject cassettePrefab;
    public GameObject cdPrefab;
    private String[] c_states = {"NORMAL", "REWIND"};
    public Sprite[] c_state_images;
    private int[] c_values = { 1, 2, 3, 4, 6, 8, 12, 14, 16, 20 };
    public Color[] c_value_colors;
    public Sprite[] c_value_icons;
    private String[] c_ranks = { "ROCK_AND_ROLL", "HIP_HOP", "CLASSICAL", "GOLD" };

    [Header("Variables")]
    public DeckDatabase deckDatabase;
    public float rewindSpeed = 0.1f;
    private float timeRemaining = 10f;
    private bool isCountdown = true;
    public int numberOfGold = 0;
    public int goldCollected = 0;
    private bool isGamePaused = false;
    private bool isGameRunning = true;
    private int score;

    private void Start()
    {
        deckDatabase.CreateDeck(c_state_images, c_value_colors, c_value_icons);
        score = 0;
        scoreText.text = "Score: " + score.ToString();

        LoadGame();
    }

    private void Update()
    {
        goldCollectedText.text = goldCollected.ToString();
        // GAME OVER
        if (CheckForGameOver())
        {
            normalPlayer.GetComponent<AudioSource>().Stop();
            rewindPlayer.GetComponent<AudioSource>().Stop();

            if (isCountdown)
                GO_GameDetailsText.text = "Score: " + score + "\n"
                + "Gold CDs Collected: " + goldCollected + "\n"
                + "Time Remaining: " + DisplayTime(timeRemaining);
            else
                GO_GameDetailsText.text = "Score: " + score + "\n"
                + "Gold CDs Collected: " + goldCollected + "\n"
                + "Time Elapsed: " + DisplayTime(timeRemaining);

            gameOverPanel.SetActive(true);
            
            if(isGameRunning)
            {
                // FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().gameOverSound);
                isGameRunning = false;
            }
            
        }
        // LEVEL COMPLETE
        if (CheckForLevelComplete())
        {
            normalPlayer.GetComponent<AudioSource>().Stop();
            rewindPlayer.GetComponent<AudioSource>().Stop();

            if (isCountdown)
                LC_GameDetailsText.text = "Score: " + score + "\n"
                + "Gold CDs Collected: " + goldCollected + "\n"
                + "Time Remaining: " + DisplayTime(timeRemaining);
            else
                LC_GameDetailsText.text = "Score: " + score + "\n"
                + "Gold CDs Collected: " + goldCollected + "\n"
                + "Time Elapsed: " + DisplayTime(timeRemaining);

            levelCompletePanel.SetActive(true);
            
            if (isGameRunning)
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().levelComplete);
                isGameRunning = false;
            }
            
            // Unlock Level
            if (!isCountdown)
            {
                string levelStr = "Level_" + (PlayerPrefs.GetInt("Level") + 1) + "_Unlocked";
                PlayerPrefs.SetInt(levelStr, 1);
            }
        }

        if (!CheckForGameOver() && !CheckForLevelComplete())
        {
            if (isCountdown)
            {
                if (timeRemaining > 0)
                {
                    timerText.text = "Time: " + DisplayTime(timeRemaining);
                    timeRemaining -= Time.deltaTime;
                }
            }
            else
            {
                timerText.text = "Time: " + DisplayTime(timeRemaining);
                timeRemaining += Time.deltaTime;
            }

            CheckCassettesInHandAndPlayers();
            CheckForMatches();
            CheckIfDeckIsEmpty();
            PlayCurrentCassetteMusic();
        }
    }

    private void LoadGame()
    {
        if (PlayerPrefs.GetInt("IsTimeCountdown") == 1)
            isCountdown = true;
        else
            isCountdown = false;

        timeRemaining = PlayerPrefs.GetFloat("Time");
        numberOfGold = PlayerPrefs.GetInt("NumberOfGOLD");
        ObjectiveInfoText.text = PlayerPrefs.GetString("Objective");
        goldCollected = 0;
        goldCollectedText.text = goldCollected.ToString();
        // TODO: 'Start Game' SFX
    }

    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (minutes < 0 && seconds < 0)
            return "00:00";
        else
            return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GetCassetteFromDeck()
    {
        GameObject newCassette = hiddenDeckPosition.transform.GetChild(hiddenDeckPosition.transform.childCount-1).gameObject;
        newCassette.transform.position = deckPosition.transform.position;
        newCassette.transform.parent = deckPosition.transform;
        FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().getCassetteFromDeck);
    }

    public void GenerateRandomCassette(GameObject newCassette)
    {
        //// VERSION 2
        int index;
        var rand = new System.Random();
        index = rand.Next(deckDatabase.deck.Count);
    }

    public void ChangeSliderColours(Draggable.Slot player, Cassette currentCassette)
    {
        if (currentCassette != null)
        {
            if (player == Draggable.Slot.NORMAL)
            {
                Slider_NormalPlayer.color = currentCassette.GetImage().color;
            }
            else if (player == Draggable.Slot.REWIND)
            {
                Slider_RewindPlayer.color = currentCassette.GetImage().color;
            }
        }
        else
        {
            if (player == Draggable.Slot.NORMAL)
            {
                Slider_NormalPlayer.color = new Color(0.8f, 0.8f, 0.8f);
            }
            else if (player == Draggable.Slot.REWIND)
            {
                Slider_RewindPlayer.color = new Color(0.8f, 0.8f, 0.8f);
            }
        }

        // FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().putCassetteInPlayer);
    }

    public void CheckCassettesInHandAndPlayers()
    {
        if (hand.gameObject.GetComponentsInChildren<Cassette>().Length < maxHandCapacity)
        {
            hand.isFull = false;
            deckButton.interactable = true;
        }
        else
        {
            hand.isFull = true;
            deckButton.interactable = false;
        }

        if (normalPlayer.gameObject.GetComponentsInChildren<Cassette>().Length > 0)
            normalPlayer.GetComponent<Dropzone>().isFull = true;
        else
            normalPlayer.GetComponent<Dropzone>().isFull = false;

        if (resultPlayer.gameObject.GetComponentsInChildren<Cassette>().Length > 0)
            resultPlayer.GetComponent<Dropzone>().isFull = true;
        else
            resultPlayer.GetComponent<Dropzone>().isFull = false;
    }

    public bool CheckForMatches()
    {
        Cassette normal = normalPlayer.GetComponentInChildren<Cassette>();
        Cassette rewind = rewindPlayer.GetComponentInChildren<Cassette>();

        if (normal != null && rewind != null)
        {
            if (normal.GetValue() == rewind.GetValue())
            {
                if (normal.GetState() != rewind.GetState())
                {
                    if (resultPlayer.transform.childCount == 0)
                        rewindButton.interactable = true;
                    else
                        rewindButton.interactable = false;
                }
            }
            else
                rewindButton.interactable = false;
        }
        else
            rewindButton.interactable = false;

        return rewindButton.interactable;
    }

    public void Rewind()
    {
        float nTime, rTime;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            rewindPlayer.GetComponent<AudioSource>().time += rewindSpeed;
            nTime = Map(normalPlayer.GetComponent<AudioSource>().time, 0, normalPlayer.GetComponent<AudioSource>().clip.length, 0.0f, 1.0f);
            rTime = Map(rewindPlayer.GetComponent<AudioSource>().time, 0, rewindPlayer.GetComponent<AudioSource>().clip.length, 0.0f, 1.0f);
        }
        else
        {
            rewindPlayer.GetComponent<AudioSource>().time -= rewindSpeed;
            nTime = Map(normalPlayer.GetComponent<AudioSource>().time, 0, normalPlayer.GetComponent<AudioSource>().clip.length, 0.0f, 1.0f);
            rTime = Map(rewindPlayer.GetComponent<AudioSource>().time, 0, rewindPlayer.GetComponent<AudioSource>().clip.length, 1.0f, 0.0f);
        }

        // Debug.Log("n time: " + Math.Round(nTime, 2) + ", r time: " + Math.Round(rTime, 2));
        if (Math.Round(nTime, 2) == Math.Round(rTime, 2))
        {
            MergeCassettes();
        }
        FindObjectOfType<RewindButton>().rewindSymbol.SetActive(true);
    }

    private static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void MergeCassettes()
    {
        Cassette normal = normalPlayer.GetComponentInChildren<Cassette>();
        Cassette rewind = rewindPlayer.GetComponentInChildren<Cassette>();

        if (normal != null && rewind != null)
        {
            for (int i=0; i<c_values.Length; i++)
            {
                if (normal.GetValue() == c_values[i])
                {
                    if(normal.GetRank() == c_ranks[2])  // Merging CLASSICALS
                    {
                        /*** Create New Cassette ***/
                        Cassette cas = new Cassette();
                        cas.CreateCassette(c_states[0], c_values[9]);
                        cas.SetImage(c_state_images[2], c_value_colors[9]);
                        cas.valueSprite = c_value_icons[9];
                        if (cas.GetState() == "NORMAL")
                        {
                            cas.music = deckDatabase.normalMusic[9];
                            cas.altMusic = deckDatabase.rewindMusic[9];
                        }                            
                        else
                        {
                            cas.music = deckDatabase.rewindMusic[9];
                            cas.altMusic = deckDatabase.normalMusic[9];
                        }
                        // cassetteDeck.Add(cas);
                        SetCassetteRank(cas);
                        /*** Instantiate ***/
                        //deck = new List<GameObject>();
                        GameObject newCassette;
                        newCassette = Instantiate(cdPrefab, resultPlayer.transform.position, Quaternion.identity, resultPlayer.transform);
                        newCassette.transform.localScale = new Vector3(1f, 1f, 1f);
                        // newCassette.GetComponent<Cassette>().CreateCassette(states[0], values[i]);
                        newCassette.GetComponent<Cassette>().SetState(cas.GetState());
                        newCassette.GetComponent<Cassette>().SetValue(cas.GetValue());
                        newCassette.GetComponent<Cassette>().SetRank(cas.GetRank());
                        // newCassette.GetComponent<Cassette>().SetImage(cas.sprite, cas.color);
                        // newCassette.GetComponent<Cassette>().valueSprite = cas.valueSprite;
                        // newCassette.GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0f);
                        // newCassette.GetComponent<Cassette>().SetText();
                        newCassette.GetComponent<Cassette>().music = cas.music;
                        newCassette.GetComponent<Cassette>().altMusic = cas.altMusic;
                        // newCassette.GetComponent<Cassette>().SetValueImage();
                        // newCassette.GetComponent<Image>().color = newCassette.GetComponent<Cassette>().color;
                        // newCassette.GetComponent<Image>().sprite = c_value_icons[10];
                        // newCassette.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
                        newCassette.GetComponent<Draggable>().parentToReturnTo = resultPlayer.transform;
                        resultPlayer.GetComponent<Dropzone>().isFull = true;
                        try
                        {
                            deckDatabase.deck.Remove(normal.gameObject);
                            deckDatabase.deck.Remove(rewind.gameObject);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("[ERROR] Removing from Deck: " + e.Message);
                        }

                        Destroy(normal.gameObject);
                        Destroy(rewind.gameObject);
                        ChangeScore(c_values[i]);
                        FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().mergeCassettes);
                        break;
                    }
                    else if (normal.GetRank() == c_ranks[3])  // GOLD
                    {

                    }
                    else // ROCK & ROLL, HIP-HOP
                    {
                        /*** Create New Cassette ***/
                        Cassette cas = new Cassette();
                        cas.CreateCassette(normal.GetState(), c_values[i + 1]);
                        cas.SetImage(normal.sprite, c_value_colors[i + 1]);
                        cas.valueSprite = c_value_icons[i + 1];
                        if (cas.GetState() == "NORMAL")
                        {
                            cas.music = deckDatabase.normalMusic[i + 1];
                            cas.altMusic = deckDatabase.rewindMusic[i + 1];
                        }                            
                        else
                        {
                            cas.music = deckDatabase.rewindMusic[i + 1];
                            cas.altMusic = deckDatabase.normalMusic[i + 1];
                        }
                        // cassetteDeck.Add(cas);
                        SetCassetteRank(cas);
                        /*** Instantiate ***/
                        //deck = new List<GameObject>();
                        GameObject newCassette;
                        newCassette = Instantiate(cassettePrefab, resultPlayer.transform.position, Quaternion.identity, resultPlayer.transform);
                        newCassette.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                        // newCassette.GetComponent<Cassette>().CreateCassette(states[0], values[i]);
                        newCassette.GetComponent<Cassette>().SetState(cas.GetState());
                        newCassette.GetComponent<Cassette>().SetValue(cas.GetValue());
                        newCassette.GetComponent<Cassette>().SetRank(cas.GetRank());
                        newCassette.GetComponent<Cassette>().SetImage(cas.sprite, cas.color);
                        newCassette.GetComponent<Cassette>().valueSprite = cas.valueSprite;
                        // newCassette.GetComponent<Cassette>().SetText();
                        newCassette.GetComponent<Cassette>().music = cas.music;
                        newCassette.GetComponent<Cassette>().altMusic = cas.altMusic;
                        newCassette.GetComponent<Cassette>().SetValueImage();
                        newCassette.GetComponent<Image>().color = newCassette.GetComponent<Cassette>().color;
                        newCassette.GetComponent<Image>().sprite = newCassette.GetComponent<Cassette>().sprite;
                        newCassette.GetComponent<Draggable>().parentToReturnTo = resultPlayer.transform;
                        resultPlayer.GetComponent<Dropzone>().isFull = true;
                        try
                        {
                            deckDatabase.deck.Remove(normal.gameObject);
                            deckDatabase.deck.Remove(rewind.gameObject);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("[ERROR] Removing from Deck: " + e.Message);
                        }

                        Destroy(normal.gameObject);
                        Destroy(rewind.gameObject);
                        ChangeScore(c_values[i]);
                        FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().mergeCassettes);
                        break;
                    }
                    
                }
            }
        }
    }

    private void SetCassetteRank(Cassette c)
    {
        if (c.GetValue() <= 4)
        {
            c.SetRank(c_ranks[0]);
        }
        else if (c.GetValue() <= 8)
        {
            c.SetRank(c_ranks[1]);
        }
        else if (c.GetValue() <= 16)
        {
            c.SetRank(c_ranks[2]);
        }
        else if (c.GetValue() == 20)
        {
            c.SetRank(c_ranks[3]);
        }
    }

    public void CheckIfDeckIsEmpty()
    {
        if (deckDatabase.deck.Count == 0 || deckDatabase.gameObject.transform.childCount == 0)
        {
            deckDatabase.deck.Clear();
            deckDatabase.CreateDeck(c_state_images, c_value_colors, c_value_icons);
        }
    }

    public bool CheckForGameOver()
    {
        /*
        1. Check if any GOLD is available
        2. Check if HAND is Full && ((Normal & Rewind are Full && No Matches) || (Result is Full))
         */
        bool hasGold = false;
        foreach (Cassette c in FindObjectsOfType<Cassette>())
        {
            if (c.GetRank() == "GOLD")
            {
                hasGold = true;
                break;
            }
        }

        if (!hasGold)
        {
            if ((hand.isFull) && 
                (!deckButton.interactable) && (
                ((normalPlayer.GetComponent<Dropzone>().isFull && rewindPlayer.GetComponent<Dropzone>().isFull) && !CheckForMatches())
                || ((normalPlayer.GetComponent<Dropzone>().isFull && rewindPlayer.GetComponent<Dropzone>().isFull) && (CheckForMatches() && resultPlayer.GetComponent<Dropzone>().isFull)) ))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public bool CheckForLevelComplete()
    {
        if(isCountdown)
        {
            if(timeRemaining > 0)
                return false;
            else
                return true;

        }
        else
        {
            if (goldCollected == numberOfGold)
                return true;
            else
                return false;
        }
    }

    public void PlayCurrentCassetteMusic()
    {
        if (normalPlayer.GetComponent<AudioSource>().isPlaying)
        {
            normalSlider.value = normalPlayer.GetComponent<AudioSource>().time;
        }
        if (rewindPlayer.GetComponent<AudioSource>().isPlaying)
        {
            rewindSlider.value = rewindPlayer.GetComponent<AudioSource>().time;
        }
    }

    public void PlayMusicFromCassette(Cassette cassette, GameObject player)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (player.GetComponent<Dropzone>().typeOfItem == Draggable.Slot.NORMAL)
            {
                player.GetComponent<AudioSource>().clip = cassette.music;
                player.GetComponent<AudioSource>().loop = true;
                normalSlider.minValue = 0;
                normalSlider.maxValue = player.GetComponent<AudioSource>().clip.length;
            }
            else if (player.GetComponent<Dropzone>().typeOfItem == Draggable.Slot.REWIND)
            {
                player.GetComponent<AudioSource>().clip = cassette.altMusic;
                player.GetComponent<AudioSource>().loop = true;
                rewindSlider.minValue = 0;
                rewindSlider.maxValue = player.GetComponent<AudioSource>().clip.length;
                rewindSlider.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                player.GetComponent<AudioSource>().pitch = 1f;
            }
        }
        else
        {
            player.GetComponent<AudioSource>().clip = cassette.music;
            player.GetComponent<AudioSource>().loop = true;

            if (player.GetComponent<Dropzone>().typeOfItem == Draggable.Slot.NORMAL)
            {
                normalSlider.minValue = 0;
                normalSlider.maxValue = player.GetComponent<AudioSource>().clip.length;
            }
            else if (player.GetComponent<Dropzone>().typeOfItem == Draggable.Slot.REWIND)
            {
                rewindSlider.minValue = 0;
                rewindSlider.maxValue = player.GetComponent<AudioSource>().clip.length;
                player.GetComponent<AudioSource>().pitch = -1f;
            }
        }

        player.GetComponent<AudioSource>().Play();
    }

    public void StopMusic(GameObject player)
    {
        player.GetComponent<AudioSource>().Stop();
        player.GetComponent<AudioSource>().loop = false;
        player.GetComponent<AudioSource>().clip = null;
    }

    public void ChangeScore(int value)
    {
        switch (value)
        {
            case 1:
                score += 10;
                break;
            case 2:
                score += 20;
                break;
            case 3:
                score += 25;
                break;
            case 4:
                score += 50;
                break;
            case 6:
                score += 60;
                break;
            case 8:
                score += 100;
                break;
            case 12:
                score += 125;
                break;
            case 14:
                score += 150;
                break;
            case 16:
                score += 200;
                break;
            case 20:
                score += 50;
                break;
            default:
                score += 0;
                break;
        }
        scoreText.text = "Score: " + score.ToString();
    }

    public void PauseOrResumeGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
