using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    public bool enableGodMode = false;
    private GameObject player;
    private PlayerAbilityManager pam;
    private PlayerHealth ph; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pam = player.GetComponent<PlayerAbilityManager>();
        ph = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enableGodMode)
        {
            pam.RestoreEnergy(100);
            CurrencyManager.aura = 99999;
            ph.health = ph.maxHealth;
        }
    }
}
