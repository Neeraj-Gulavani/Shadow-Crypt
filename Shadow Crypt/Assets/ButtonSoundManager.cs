using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public static ButtonSoundManager instance;
    public AudioClip buttonHoverSfx;
    public AudioClip buttonClickSfx;
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayHover()
    {
         aud.PlayOneShot(buttonHoverSfx);
    }

public void PlayClick()
{
    aud.PlayOneShot(buttonClickSfx);
}

}
