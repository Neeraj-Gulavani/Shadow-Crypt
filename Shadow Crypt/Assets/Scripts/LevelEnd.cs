using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            playerTeleport p = c.GetComponent<playerTeleport>();
            p.NextScene();
            //StartCoroutine(p.SceneChangeTp());
        }
    }
}
