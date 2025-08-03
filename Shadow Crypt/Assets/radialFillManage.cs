using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radialFillManage : MonoBehaviour
{
    public float costValue;
    private Image img;
    public float fillSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float toFill =  Mathf.Clamp(PlayerAbilityManager.energy / costValue,0f,1f);
        img.fillAmount = Mathf.Lerp(img.fillAmount, toFill, fillSpeed * Time.deltaTime);
    }
}
