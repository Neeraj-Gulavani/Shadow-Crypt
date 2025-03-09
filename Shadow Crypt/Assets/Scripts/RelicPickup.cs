using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : MonoBehaviour
{
    public GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Cursor.lockState = CursorLockMode.None;
            win.SetActive(true);
        }
    }
}
