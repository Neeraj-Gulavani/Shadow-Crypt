using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Lightning", menuName ="Abilities/Lightning")]
public class Lightning : AbilityBase
{
    public GameObject vfx;
    public GameObject strikeHeight;
    public float radius = 10f;
    public override void Activate(Transform player)
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, radius);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Enemy")) return;
        GameObject theVfx = Instantiate(vfx, c.transform.position, Quaternion.identity);
        }
    }
}
