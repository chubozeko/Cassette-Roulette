using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.Slot typeOfItem = Draggable.Slot.NORMAL;
    public bool isFull = false;
    public int maxHandCapacity = 7;
    public Cassette currentCassetteInPlayer = null;

    private void FixedUpdate()
    {
        if (transform.childCount == 0)
        {
            isFull = false;
            currentCassetteInPlayer = null;
            if (typeOfItem == Draggable.Slot.NORMAL || typeOfItem == Draggable.Slot.REWIND)
                FindObjectOfType<GameController>().StopMusic(gameObject);
        }

        FindObjectOfType<GameController>().ChangeSliderColours(typeOfItem, currentCassetteInPlayer);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = transform;

            if (transform.childCount == 0)
                isFull = false;

            FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().putCassetteInPlayer);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log( eventData.pointerDrag.name + " was dropped on " + gameObject.name );

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            if (typeOfItem == Draggable.Slot.NORMAL || typeOfItem == Draggable.Slot.REWIND)
            {
                if (!isFull)
                {
                    d.parentToReturnTo = transform;
                    isFull = true;
                    currentCassetteInPlayer = eventData.pointerDrag.GetComponent<Cassette>();
                    // FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().putCassetteInPlayer);
                    FindObjectOfType<GameController>().PlayMusicFromCassette(eventData.pointerDrag.GetComponent<Cassette>(), gameObject);
                }
            }

            if (typeOfItem == Draggable.Slot.RESULT)
            {
                if (!isFull)
                {
                    // d.parentToReturnTo = transform;
                    isFull = true;
                    // currentCassetteInPlayer = eventData.pointerDrag.GetComponent<Cassette>();
                }
            }

            if (typeOfItem == Draggable.Slot.HAND)
            {
                if (!isFull)
                {
                    d.parentToReturnTo = transform;
                    FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().getCassetteFromDeck);
                }
            }

            if (typeOfItem == Draggable.Slot.COMPLETE)
            {
                if(d.gameObject.GetComponent<Cassette>().GetRank() == "GOLD")
                {
                    d.parentToReturnTo = transform;
                    Destroy(d.gameObject);
                    FindObjectOfType<AudioManager>().PlayOneShot(FindObjectOfType<AudioManager>().putCDInStorage);
                    FindObjectOfType<GameController>().goldCollected++;
                    FindObjectOfType<GameController>().ChangeScore(20);
                }
            }
        }
        /*
        if (transform.childCount == 0)
            isFull = false;
        */

        if (typeOfItem == Draggable.Slot.HAND)
        {
            if (gameObject.GetComponentsInChildren<Cassette>().Length < maxHandCapacity)
                isFull = false;
            else
                isFull = true;
        }
        
        FindObjectOfType<GameController>().ChangeSliderColours(typeOfItem, currentCassetteInPlayer);

        // Debug.Log(gameObject.name + " contains " + gameObject.GetComponentsInChildren<Draggable>().Length+1 + " cassettes.");
        // throw new NotImplementedException();
    }
}
