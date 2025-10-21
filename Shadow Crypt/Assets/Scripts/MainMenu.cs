using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject cpanel;
    public Animator fadeAnim;
    public bool canChangeScene;
    AsyncOperation operation;
    public void Controls() {
        cpanel.SetActive(true);
    }
    public void CloseControls() {
        cpanel.SetActive(false);
    }
    public void Play()
    {
        //SceneManager.LoadScene("Scene2");
        
        StartCoroutine(LoadNextScene());
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("hasStarted", 1);
        Play();
    }
    private IEnumerator LoadNextScene()
    {

        fadeAnim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        operation.allowSceneActivation = true;
    }
    public void Quit() {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {

        int scenetoLoad = PlayerPrefs.HasKey("CurrentScene")? PlayerPrefs.GetInt("CurrentScene") : SceneManager.GetActiveScene().buildIndex + 1;
         operation = SceneManager.LoadSceneAsync(scenetoLoad);
        operation.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
