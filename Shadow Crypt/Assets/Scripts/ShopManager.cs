using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    private bool shopEnabled=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();

        }
    }

    public void ToggleShop()
    {

        if (!shopEnabled)
        {
            shopPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            shopPanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;

        }
        shopEnabled = !shopEnabled;
    }
}
