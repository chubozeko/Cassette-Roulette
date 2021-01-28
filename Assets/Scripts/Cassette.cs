using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cassette : MonoBehaviour
{
    public string state;
    public int value;
    public string rank;
    public Image image;
    public Image valueImage;
    public Sprite valueSprite;
    public Sprite sprite;
    public Color color;
    public Text text;
    public AudioClip music;
    public AudioClip altMusic;

    private void Start()
    {
        image = GetComponent<Image>();
        valueImage = GetComponentInChildren<Image>();
    }

    public Cassette() { }

    public void CreateCassette(string state, int value) // Image image, string rank
    {
        this.state = state;
        this.value = value;
        // this.image = gameObject.AddComponent<Image>();
        // this.rank = rank;
    }

    public void SetState(string state) { this.state = state; }
    public string GetState() { return this.state; }

    public void SetValue(int value) { this.value = value; }
    public int GetValue() { return this.value; }

    public void SetRank(string rank) { this.rank = rank; }
    public string GetRank() { return this.rank; }

    public void SetImage(Sprite sprite, Color color)
    {
        // this.image.sprite = sprite;
        // this.image.color = color;
        this.sprite = sprite;
        this.color = color;
    }
    public Image GetImage() { return this.image; }

    public void SetText() { this.text.text = this.value.ToString(); }

    public void SetValueImage()
    { 
        this.valueImage.sprite = this.valueSprite;
        this.valueImage.color = this.color;
        gameObject.GetComponentInChildren<Image>().sprite = this.valueSprite;
        gameObject.GetComponentInChildren<Image>().color = this.color;
    }

}
