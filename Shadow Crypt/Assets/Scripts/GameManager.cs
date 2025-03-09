using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPos;
    public SpriteRenderer sr,sr1,sr2;
    public GameObject deathui;
    public GameObject pauseui;
    public bool ispaused=false;
    public void Respawn() {
        
     SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }
    public void Quit() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale=1;
    }

    public void GotoMainMenu() {
             SceneManager.LoadScene("MainMenu");
             Time.timeScale=1;   

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ispaused=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if (ispaused) {
                Time.timeScale=1;
                pauseui.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else {
                Time.timeScale=0;
                pauseui.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
            ispaused=!ispaused;
    }

}

