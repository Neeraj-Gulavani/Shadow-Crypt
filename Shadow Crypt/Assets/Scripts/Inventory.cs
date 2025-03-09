using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public static Dictionary<string,int> inv=new Dictionary<string, int>();
    public TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        inv.Add("health",0);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text=inv["health"].ToString();
    }

    public static void AddItem(string itemName,int quantity) {
        inv[itemName]+=quantity;
    }

}
