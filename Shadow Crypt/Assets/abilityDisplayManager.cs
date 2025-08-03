using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class abilityDisplayManager : MonoBehaviour
{
    public static bool lightningEnabled = false;
    public GameObject lightningDisplayboundary, lightningGrayIcon; 


    public static bool soulDrainEnabled = false;
    public GameObject soulDrainDisplayboundary, soulDrainGrayIcon; 

    public static bool rogueEnabled = false;
    public GameObject rogueDisplayboundary, rogueGrayIcon;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("lightningEnabled"))
        {
            lightningEnabled = PlayerPrefs.GetInt("lightningEnabled") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("soulDrainEnabled"))
        {
            soulDrainEnabled = PlayerPrefs.GetInt("soulDrainEnabled") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("rogueEnabled"))
        {
            rogueEnabled = PlayerPrefs.GetInt("rogueEnabled") == 1 ? true : false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
        if (lightningEnabled)
        {
            pam.UnlockLightning();
        }
        if (soulDrainEnabled)
        {
            pam.UnlockSoulDrain();
        }
        if (rogueEnabled)
        {
            pam.UnlockRogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lightningEnabled)
        {
            PlayerPrefs.SetInt("lightningEnabled", 1);
            PlayerPrefs.Save();
            lightningDisplayboundary.SetActive(true);
            lightningGrayIcon.SetActive(true);
        }

        if (soulDrainEnabled)
        {
            PlayerPrefs.SetInt("soulDrainEnabled", 1);
            PlayerPrefs.Save();
            soulDrainDisplayboundary.SetActive(true);
            soulDrainGrayIcon.SetActive(true);
        }
         if (rogueEnabled)
        {
            PlayerPrefs.SetInt("rogueEnabled", 1);
            PlayerPrefs.Save();
            rogueDisplayboundary.SetActive(true);
            rogueGrayIcon.SetActive(true);
        }
    }
}
