using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public UIManager man;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 50;

        man = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        man.updateHealthGauge((float)currentHealth/maxHealth);
    }
}
