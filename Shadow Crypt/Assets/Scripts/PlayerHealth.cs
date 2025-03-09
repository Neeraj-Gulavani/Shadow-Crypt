using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth=100f;
    public SpriteRenderer sr,sr1,sr2;
    public GameObject spawnPos;
    public float flashDuration;
    public CinemachineImpulseSource impulseSrc;
    public Image h1,h2,h3,h4;

    public GameObject deathui;
    public void TakeDamage(float amt) {
        Shake(0.2f);

        StartCoroutine(DamageFlash());
            if (amt>=health) {
            Die();
            health-=amt;
        } else {
            health-=amt;
        }
        DispHearts();
    }

    public void Heal(float amt) {
        if (health+amt>maxHealth) {
            health = maxHealth;
        } else {
        health+=amt;
        }
        DispHearts();
    }
    public void Die() {
        
        deathui.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathui = GameObject.Find("Canvas").transform.Find("DeathUI")?.gameObject;
        deathui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        health=maxHealth;
        sr = GetComponent<SpriteRenderer>();
        h1 = GameObject.Find("Canvas").transform.Find("HUD")?.transform.Find("1").GetComponent<Image>();
    h2 = GameObject.Find("Canvas").transform.Find("HUD")?.transform.Find("2").GetComponent<Image>();
    h3 = GameObject.Find("Canvas").transform.Find("HUD")?.transform.Find("3").GetComponent<Image>();
    h4 = GameObject.Find("Canvas").transform.Find("HUD")?.transform.Find("4").GetComponent<Image>();
    }

    void Shake(float intensity=1f) {
        impulseSrc.GenerateImpulseWithForce(intensity);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (Inventory.inv["health"]>0) {
                Heal(40);
                Inventory.inv["health"]-=1;
            }
        }
    }

    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = Color.white;
    }

    void DispHearts() {
        if (health>30) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(true);
        } else if (health>20 && health<31) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(false);
        } else if (health<21 && health>10) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        } else if (health<11 && health>0) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(false);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        }
        else if (health<=1) {
            h1.gameObject.SetActive(false);
            h2.gameObject.SetActive(false);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        }
    }

}
