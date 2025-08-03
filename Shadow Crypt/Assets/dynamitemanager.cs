using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class dynamitemanager : MonoBehaviour

{
    // Start is called before the first frame update
    public bool haspicked = false;
    private SpriteRenderer sr;
    private GameObject player;
    public Sprite Ruined_Gate;
    public GameObject Explosion_vfx;
    public bool hasputdown=false;
    public Animator sparks;
     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void pickup()
    {
        if (Input.GetKeyDown(KeyCode.G) && !haspicked)
        {
            haspicked = true;

            transform.SetParent(player.transform);
            sr.color = new Color(1f, 1f, 1f, 0f);
        }

    }
    void putdown(GameObject gate, Vector3 pos)
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        hasputdown = true;
        sparks.SetBool("hasputdown",true);
        Debug.Log("niggers");
        sr.color = new Color(1f, 1f, 1f, 1f);
        transform.position = pos;
        transform.SetParent(null);
        // }
        StartCoroutine(Destroy_Gate(gate));

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickup();
        }
        if (collision.CompareTag("Gate"))
        {

            putdown(collision.gameObject, collision.ClosestPoint(player.transform.position));
        }

    }

    IEnumerator Destroy_Gate(GameObject gate)
    {
        yield return new WaitForSeconds(5f);
        gate.GetComponent<SpriteRenderer>().sprite = Ruined_Gate;
        Instantiate(Explosion_vfx, transform.position, Quaternion.identity);
        hasputdown = false;
        sparks.SetBool("hasputdown",false);
        Destroy(gameObject, 0f);

    }
}
