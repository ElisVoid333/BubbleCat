using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHPControl : MonoBehaviour
{
    [SerializeField] Slider Currentslider;

    private void Start()
    {
        Currentslider = this.gameObject.GetComponent<Slider>();
    }

    public void SetupHPBar(float Maxvalue)
    {
        Currentslider.maxValue = Maxvalue;
        Currentslider.value = Maxvalue;
    }

    public void UpdateHP(float currenthp)
    {
        Currentslider.value = currenthp;
    }

}
