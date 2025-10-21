using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        bool hasPlayed = PlayerPrefs.GetInt("hasStarted") == 1 ? true : false;
        gameObject.SetActive(hasPlayed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
