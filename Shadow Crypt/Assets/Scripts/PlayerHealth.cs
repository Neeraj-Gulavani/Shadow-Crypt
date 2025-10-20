using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
public class PlayerHealth : MonoBehaviour
{
    private Light2D globalLight;
    public Light2D blindedLight;
    PlayerControls controls;
    public float health;
    public float maxHealth=100f;
    public SpriteRenderer sr,sr1,sr2;
    public GameObject spawnPos;
    public float flashDuration;
    public CinemachineImpulseSource impulseSrc;
    public Image h1,h2,h3,h4;
    public Image fillBar;
    public GameObject deathui;

    public GameObject imapctVfx;
    private float targetFill;
    public float fillSpeed = 5f;

    public float lowhealth=10f;
    public GameObject lowhealthpanel;
    public void TakeDamage(float amt)
    {
        Shake(1f);
        if (PlayerMovement.isparry) return;
        PlayerVibration.instance.Vibrate(0.6f, 1.0f, 0.25f); 
        StartCoroutine(DamageFlash());
        if (amt >= health)
        {
            Die();
            health -= amt;
        }
        else
        {
            health -= amt;
        }
        //DispHearts();
        HealthBarUpdate();
    }

    public void ImpactFx(Vector2 pos) {
        GameObject vfx = Instantiate(imapctVfx, pos, Quaternion.identity);
        Destroy(vfx,1f);
    }

    public void Heal(float amt) {
        if (health+amt>maxHealth) {
            health = maxHealth;
        } else {
        health+=amt;
        }
        //DispHearts();
        HealthBarUpdate();
    }
    public void Die()
    {

        deathui.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.SetActive(false);
    }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Heal.started += ctx => PlayerHeal();
         globalLight = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
    }
     void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        deathui = GameObject.Find("Canvas").transform.Find("DeathUI")?.gameObject;
        deathui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        health=maxHealth;
        targetFill = health;
        sr = GetComponent<SpriteRenderer>();
    }

    public void Shake(float intensity=1f) {
        impulseSrc.GenerateImpulseWithForce(intensity);
    }
    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, targetFill, fillSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        if (health <= lowhealth)
        {
            lowhealthpanel.SetActive(true);
        }
        else
        {
            lowhealthpanel.SetActive(false);
        }
    }
    
    public void PlayerHeal()
    {
        if (Inventory.inv["health"] > 0)
            {
                Heal(15);
                Inventory.inv["health"] -= 1;
            }
    }

    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = Color.white;
    }


    void HealthBarUpdate() {
        targetFill = health / maxHealth;
        
    }
    public void setHealth(float health)
    {
        this.health = health;
        HealthBarUpdate();
    }

    public void ApplyBlindEffect()
    {
        StartCoroutine(blindedLightActivate(0.5f, 0.5f));
        StartCoroutine(FadeLight(0.02f, 0.5f));
        StartCoroutine(resetBlind());

    }

    IEnumerator resetBlind()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(blindedLightActivate(0f, 0.5f));
        StartCoroutine(FadeLight(1f, 0.5f));
    }

    IEnumerator blindedLightActivate(float targetIntensity, float duration)
    {
        float start = blindedLight.intensity;
        float time = 0f;

    while (time < duration)
    {
        time += Time.deltaTime;
        blindedLight.intensity = Mathf.Lerp(start, targetIntensity, time / duration);
        yield return null;
    }

    blindedLight.intensity = targetIntensity;
    }

    IEnumerator FadeLight(float targetIntensity, float duration)
{
    float start = globalLight.intensity;
    float time = 0f;

    while (time < duration)
    {
        time += Time.deltaTime;
        globalLight.intensity = Mathf.Lerp(start, targetIntensity, time / duration);
        yield return null;
    }

    globalLight.intensity = targetIntensity;
}

}
