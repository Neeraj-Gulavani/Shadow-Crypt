using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrencyManager : MonoBehaviour
{
    public static float aura = 0f;
    public TMP_Text auraDisplay;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("aura"))
        {
            PlayerPrefs.SetFloat("aura", 0f);
            PlayerPrefs.Save();
        }
        else
        {
            aura = PlayerPrefs.GetFloat("aura");
        }
    }

    // Update is called once per frame
    void Update()
    {
        auraDisplay.text = aura.ToString();
    }

    public static bool Debit(float amt)
    {
        aura = PlayerPrefs.GetFloat("aura");
        if (aura >= amt)
        {
            aura -= amt;
            PlayerPrefs.SetFloat("aura", aura);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            Debug.Log("Not Enough Aura!");
            return false;
        }
    }

    public static void Credit(float amt)
    {
        aura = PlayerPrefs.GetFloat("aura");
        aura += amt;
        PlayerPrefs.SetFloat("aura", aura);
        PlayerPrefs.Save();
    }
}
