using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDatabase : MonoBehaviour
{
    public List<int> values;
    public List<string> states;
    public List<string> ranks;
    public GameObject cassettePrefab;
    public List<GameObject> deck;
    private Cassette tempDeck;
    private List<Cassette> cassetteDeck;

    [Header("Audio Files")]
    public AudioClip[] normalMusic;
    public AudioClip[] rewindMusic;

    void Start()
    {
        CreateValuesList();
        CreateStatesList();
        CreateRanksList();
    }

    private void CreateValuesList()
    {
        values = new List<int>();
        values.Add(1);
        values.Add(2);
        values.Add(3);
        values.Add(4);
        values.Add(6);
        values.Add(8);
        values.Add(12);
        values.Add(14);
        values.Add(16);
        values.Add(20);
    }

    private void CreateStatesList()
    {
        states = new List<string>();
        states.Add("NORMAL");
        states.Add("REWIND");
    }

    private void CreateRanksList()
    {
        ranks = new List<string>();
        ranks.Add("HIP_HOP");
        ranks.Add("ROCK_AND_ROLL");
        ranks.Add("CLASSICAL");
        ranks.Add("GOLD");
    }

    public void CreateCassettes(Sprite[] c_state_images, Color[] c_value_colors)
    {
        GameObject newCassette;
        // Values 1 -> 8
        for (int i=0; i<6; i++)
        {
            // Create two cassettes of each State
            // NORMAL
            newCassette = Instantiate(cassettePrefab, transform.position, Quaternion.identity, transform);
            newCassette.GetComponent<Cassette>().CreateCassette(states[0], values[i]); //, cassettePrefab.GetComponent<Image>());
            newCassette.GetComponent<Cassette>().SetImage(c_state_images[0], c_value_colors[i]);
            deck.Add(newCassette);
            // REWIND
            newCassette = Instantiate(cassettePrefab, transform.position, Quaternion.identity, transform);
            newCassette.GetComponent<Cassette>().CreateCassette(states[1], values[i]); //, cassettePrefab.GetComponent<Image>());
            newCassette.GetComponent<Cassette>().SetImage(c_state_images[1], c_value_colors[i]);
            deck.Add(newCassette);
        }

        // Set Ranks
        foreach(GameObject c in deck)
        {
            if (c.GetComponent<Cassette>().GetValue() <= 4)
            {
                c.GetComponent<Cassette>().SetRank(ranks[0]);
            }
            else if (c.GetComponent<Cassette>().GetValue() <= 8)
            {
                c.GetComponent<Cassette>().SetRank(ranks[1]);
            }
            else if (c.GetComponent<Cassette>().GetValue() <= 16)
            {
                c.GetComponent<Cassette>().SetRank(ranks[2]);
            }
            else if (c.GetComponent<Cassette>().GetValue() == 20)
            {
                c.GetComponent<Cassette>().SetRank(ranks[3]);
            }
            cassetteDeck.Add(c.GetComponent<Cassette>());
        }
    }

    public void CreateDeck(Sprite[] c_state_images, Color[] c_value_colors, Sprite[] c_value_icons)
    {
        cassetteDeck = new List<Cassette>();
        Cassette cas, cas2;
        // Cassettes (Value, State): 
        // -> {1, NORMAL}; {1, NORMAL}; {1, REWIND}; {1, REWIND}; 
        for (int j = 0; j < 2; j++)
        {
            // NORMAL
            cas = new Cassette();
            cas.CreateCassette(states[0], values[0]);
            cas.SetImage(c_state_images[0], c_value_colors[0]);
            cas.music = normalMusic[0];
            cas.altMusic = rewindMusic[0];
            cas.valueSprite = c_value_icons[0];
            cassetteDeck.Add(cas);
            // REWIND
            cas2 = new Cassette();
            cas2.CreateCassette(states[1], values[0]);
            cas2.SetImage(c_state_images[1], c_value_colors[0]);
            cas2.music = rewindMusic[0];
            cas2.altMusic = normalMusic[0];
            cas2.valueSprite = c_value_icons[0];
            cassetteDeck.Add(cas2);
        }
        // -> {2, NORMAL}; {2, NORMAL}; {2, REWIND}; {2, REWIND}; 
        for (int j = 0; j < 2; j++)
        {
            // NORMAL
            cas = new Cassette();
            cas.CreateCassette(states[0], values[1]);
            cas.SetImage(c_state_images[0], c_value_colors[1]);
            cas.music = normalMusic[1];
            cas.altMusic = rewindMusic[1];
            cas.valueSprite = c_value_icons[1];
            cassetteDeck.Add(cas);
            // REWIND
            cas2 = new Cassette();
            cas2.CreateCassette(states[1], values[1]);
            cas2.SetImage(c_state_images[1], c_value_colors[1]);
            cas2.music = rewindMusic[1];
            cas2.altMusic = normalMusic[1];
            cas2.valueSprite = c_value_icons[1];
            cassetteDeck.Add(cas2);
        }
        // -> {3, NORMAL}; {3, NORMAL}; {3, REWIND}; {3, REWIND}; 
        for (int j = 0; j < 2; j++)
        {
            // NORMAL
            cas = new Cassette();
            cas.CreateCassette(states[0], values[2]);
            cas.SetImage(c_state_images[0], c_value_colors[2]);
            cas.music = normalMusic[2];
            cas.altMusic = rewindMusic[2];
            cas.valueSprite = c_value_icons[2];
            cassetteDeck.Add(cas);
            // REWIND
            cas2 = new Cassette();
            cas2.CreateCassette(states[1], values[2]);
            cas2.SetImage(c_state_images[1], c_value_colors[2]);
            cas2.music = rewindMusic[2];
            cas2.altMusic = normalMusic[2];
            cas2.valueSprite = c_value_icons[2];
            cassetteDeck.Add(cas2);
        }
        // -> {4, NORMAL}; {4, NORMAL}; {4, REWIND}; {4, REWIND};
        for (int j = 0; j < 2; j++)
        {
            // NORMAL
            cas = new Cassette();
            cas.CreateCassette(states[0], values[3]);
            cas.SetImage(c_state_images[0], c_value_colors[3]);
            cas.music = normalMusic[3];
            cas.altMusic = rewindMusic[3];
            cas.valueSprite = c_value_icons[3];
            cassetteDeck.Add(cas);
            // REWIND
            cas2 = new Cassette();
            cas2.CreateCassette(states[1], values[3]);
            cas2.SetImage(c_state_images[1], c_value_colors[3]);
            cas2.music = rewindMusic[3];
            cas2.altMusic = normalMusic[3];
            cas2.valueSprite = c_value_icons[3];
            cassetteDeck.Add(cas2);
        }

        // -> {6, NORMAL}; {6, REWIND};
        // NORMAL
        cas = new Cassette();
        cas.CreateCassette(states[0], values[4]);
        cas.SetImage(c_state_images[0], c_value_colors[4]);
        cas.music = normalMusic[4];
        cas.altMusic = rewindMusic[4];
        cas.valueSprite = c_value_icons[4];
        cassetteDeck.Add(cas);
        // REWIND
        cas2 = new Cassette();
        cas2.CreateCassette(states[1], values[4]);
        cas2.SetImage(c_state_images[1], c_value_colors[4]);
        cas2.music = rewindMusic[4];
        cas2.altMusic = normalMusic[4];
        cas2.valueSprite = c_value_icons[4];
        cassetteDeck.Add(cas2);
        // -> {8, NORMAL}; {8, REWIND};
        // NORMAL
        cas = new Cassette();
        cas.CreateCassette(states[0], values[5]);
        cas.SetImage(c_state_images[0], c_value_colors[5]);
        cas.music = normalMusic[5];
        cas.altMusic = rewindMusic[5];
        cas.valueSprite = c_value_icons[5];
        cassetteDeck.Add(cas);
        // REWIND
        cas2 = new Cassette();
        cas2.CreateCassette(states[1], values[5]);
        cas2.SetImage(c_state_images[1], c_value_colors[5]);
        cas2.music = rewindMusic[5];
        cas2.altMusic = normalMusic[5];
        cas2.valueSprite = c_value_icons[5];
        cassetteDeck.Add(cas2);
        // -> {12, NORMAL}; {12, REWIND};
        // NORMAL
        cas = new Cassette();
        cas.CreateCassette(states[0], values[6]);
        cas.SetImage(c_state_images[0], c_value_colors[6]);
        cas.music = normalMusic[6];
        cas.altMusic = rewindMusic[6];
        cas.valueSprite = c_value_icons[6];
        cassetteDeck.Add(cas);
        // REWIND
        cas2 = new Cassette();
        cas2.CreateCassette(states[1], values[6]);
        cas2.SetImage(c_state_images[1], c_value_colors[6]);
        cas2.music = rewindMusic[6];
        cas2.altMusic = normalMusic[6];
        cas2.valueSprite = c_value_icons[6];
        cassetteDeck.Add(cas2);
        // -> {14, NORMAL}; {14, REWIND};
        // NORMAL
        cas = new Cassette();
        cas.CreateCassette(states[0], values[7]);
        cas.SetImage(c_state_images[0], c_value_colors[7]);
        cas.music = normalMusic[7];
        cas.altMusic = rewindMusic[7];
        cas.valueSprite = c_value_icons[7];
        cassetteDeck.Add(cas);
        // REWIND
        cas2 = new Cassette();
        cas2.CreateCassette(states[1], values[7]);
        cas2.SetImage(c_state_images[1], c_value_colors[7]);
        cas2.music = rewindMusic[7];
        cas2.altMusic = normalMusic[7];
        cas2.valueSprite = c_value_icons[7];
        cassetteDeck.Add(cas2);
        // -> {16, NORMAL}; {16, REWIND};
        // NORMAL
        cas = new Cassette();
        cas.CreateCassette(states[0], values[8]);
        cas.SetImage(c_state_images[0], c_value_colors[8]);
        cas.music = normalMusic[8];
        cas.altMusic = rewindMusic[8];
        cas.valueSprite = c_value_icons[8];
        cassetteDeck.Add(cas);
        // REWIND
        cas2 = new Cassette();
        cas2.CreateCassette(states[1], values[8]);
        cas2.SetImage(c_state_images[1], c_value_colors[8]);
        cas2.music = rewindMusic[8];
        cas2.altMusic = normalMusic[8];
        cas2.valueSprite = c_value_icons[8];
        cassetteDeck.Add(cas2);

        // Set Ranks
        foreach (Cassette c in cassetteDeck) // foreach (GameObject c in deck)
        {
            if (c.GetValue() <= 4)
            {
                c.SetRank(ranks[0]);
            }
            else if (c.GetValue() <= 8)
            {
                c.SetRank(ranks[1]);
            }
            else if (c.GetValue() <= 16)
            {
                c.SetRank(ranks[2]);
            }
            else if (c.GetValue() == 20)
            {
                c.SetRank(ranks[3]);
            }
            // cassetteDeck.Add(c.GetComponent<Cassette>());
        }

        ShuffleDeck();

        // Instantiate
        deck = new List<GameObject>();
        GameObject newCassette;
        foreach (Cassette c in cassetteDeck)
        {
            newCassette = Instantiate(cassettePrefab, transform.position, Quaternion.identity, transform);
            // newCassette.GetComponent<Cassette>().CreateCassette(states[0], values[i]);
            newCassette.GetComponent<Cassette>().SetState(c.GetState());
            newCassette.GetComponent<Cassette>().SetValue(c.GetValue());
            newCassette.GetComponent<Cassette>().SetRank(c.GetRank());
            newCassette.GetComponent<Cassette>().SetImage(c.sprite, c.color);
            newCassette.GetComponent<Cassette>().valueSprite = c.valueSprite;
            // newCassette.GetComponent<Cassette>().SetText();
            newCassette.GetComponent<Cassette>().music = c.music;
            newCassette.GetComponent<Cassette>().altMusic = c.altMusic;
            newCassette.GetComponent<Cassette>().SetValueImage();
            newCassette.GetComponent<Image>().color = newCassette.GetComponent<Cassette>().color;
            newCassette.GetComponent<Image>().sprite = newCassette.GetComponent<Cassette>().sprite;
            deck.Add(newCassette);
        }
    }

    public void ShuffleDeck()
    {
        // tempDeck = new List<GameObject>();
        for(int l=0; l<cassetteDeck.Count; l++)
        {
            tempDeck = cassetteDeck[l];
            int rand = Random.Range(l, cassetteDeck.Count);
            cassetteDeck[l] = cassetteDeck[rand];
            cassetteDeck[rand] = tempDeck;
        }
    }
}
