using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioGenerate : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("HMM BUTTON SELECTED");
        PlayHoverSound();
    }

    void PlayHoverSound()
    {
        ButtonSoundManager.instance.PlayHover();
    }
}
