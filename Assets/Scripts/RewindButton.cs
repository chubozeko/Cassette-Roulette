using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindButton : MonoBehaviour
{
    public bool isRewindPressed = false;
    public GameObject rewindSymbol;
    void Start()
    {
        
    }

    void Update()
    {
        if (isRewindPressed && gameObject.GetComponent<Button>().interactable)
        {
            FindObjectOfType<GameController>().Rewind();
        }
    }
    public void OnPointerDownRewindButton()
    {
        if (gameObject.GetComponent<Button>().interactable)
            isRewindPressed = true;
        // rewindSymbol.SetActive(true);
    }
    public void OnPointerUpRewindButton()
    {
        isRewindPressed = false;
        rewindSymbol.SetActive(false);
    }


}
