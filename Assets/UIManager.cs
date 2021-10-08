using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public Slider boostGauge;

    public Slider healthgauge;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGauge(float amount){
        boostGauge.value = amount;
    }
    public void updateHealthGauge(float amount){
        healthgauge.value = amount;
    }
}
