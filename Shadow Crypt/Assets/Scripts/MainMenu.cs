using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject cpanel;
    public void Controls() {
        cpanel.SetActive(true);
    }
    public void CloseControls() {
        cpanel.SetActive(false);
    }
    public void Play() {
        SceneManager.LoadScene("Scene1");
    }

    public void Quit() {
        SceneManager.LoadScene("MainMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
