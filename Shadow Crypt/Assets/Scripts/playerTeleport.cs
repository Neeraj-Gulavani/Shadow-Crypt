
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerTeleport : MonoBehaviour
{
    public GameObject tpVfx;
    /*
    public GameObject loadingScreen;
    public GameObject fadeObj;
    public Image fadeImage;
    public float fadeDuration = 1f;
    */
    public Animator fadeAnim;
    public static bool canChangeScene = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartEffect");
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
        public IEnumerator SceneChangeTp()
        {
            tpVfx.SetActive(true);
            yield return new WaitForSeconds(2f);
            tpVfx.SetActive(false);
            StartCoroutine("Fade");
            loadingScreen.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);


                if (operation.progress >= 0.9f)
                {

                    if (Input.anyKeyDown)
                    {
                        operation.allowSceneActivation = true;
                    }
                }
                yield return null;

            }
        }
    */
    public void NextScene()
    {
        StartCoroutine(LoadNextScene());
    }

    
private IEnumerator LoadNextScene()
    {
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex + 1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    operation.allowSceneActivation = false;
    tpVfx.SetActive(true);
        yield return new WaitForSeconds(2f);
        tpVfx.SetActive(false);
        fadeAnim.SetBool("fadeOut", true);
        /*
        while (!canChangeScene)
        {
            yield return null; // don't freeze the game
        }
        */
        yield return new WaitForSeconds(1f);
        operation.allowSceneActivation = true;
        canChangeScene = false;
}
    

    public IEnumerator StartEffect()
    {
        tpVfx.SetActive(true);
        yield return new WaitForSeconds(2f);
        tpVfx.SetActive(false);
    }
/*
    private IEnumerator Fade()
    {
        fadeObj.SetActive(true);
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1, time / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        fadeObj.SetActive(false);
    }
    */
    
}
