using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public Slider boostGauge;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGauge(float amount){
        boostGauge.value = amount;
    }
}
