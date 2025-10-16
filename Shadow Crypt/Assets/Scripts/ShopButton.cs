using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopButton : MonoBehaviour
{
    public int cost;
    public UnityEvent onPurchase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickBuy()
    {
        if (CurrencyManager.Debit(cost))
        {
            onPurchase?.Invoke();
        }
        else
        {
            //  Debug.Log("Not Enough Aura!");
        }
    }
}
