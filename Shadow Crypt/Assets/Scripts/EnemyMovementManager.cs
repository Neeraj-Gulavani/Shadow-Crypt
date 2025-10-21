using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyMovementManager : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip alertSfx;
    private AIPath aiPath;
    private Transform player;
    public float detectRange=5f;
    public bool hasDetected=false;
    private AIDestinationSetter aidest;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aiPath = GetComponent<AIPath>();
        aiPath.enabled=false;
        aidest = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (player==null) return;
        float playerDistance=Vector2.Distance(transform.position,player.position);
        if (!hasDetected && playerDistance<=detectRange) {
            if (alertSfx!=null && aud!=null)
            {
                aud.PlayOneShot(alertSfx);
            }
            aiPath.enabled = true;
            aidest.target =player;
            hasDetected=true;
        } 
    }

 



 
}
