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
    public void Respawn() {
        
     SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }
    public void Quit() {
        Application.Quit();
    }

    public void GotoMainMenu() {
             SceneManager.LoadScene("MainMenu");   

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
