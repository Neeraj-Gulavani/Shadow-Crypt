using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerChange : MonoBehaviour
{
    public Transform gate;
    public int orderSet=9;

    public GameObject otherColl;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            otherColl.SetActive(true);
            gate.GetComponent<SpriteRenderer>().sortingOrder = orderSet;
            gameObject.SetActive(false);
        }
    }
}
