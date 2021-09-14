using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class boostManager : MonoBehaviour
{
    public CinemachineDollyCart cartman;
    public Material enginePlumes;

    public float mainSpeed;
    public float boostSpeed;
    
    public float hueOffset;
    public bool isBoosting;

    public float normalFOV;

    public float boostFOV;

    public float currentFOV;

    public float fovZoomTime;

    private float targetFOV;

    private float fovSmoothing;

    public CameraShake cs;

    public Camera maincam;
    // Start is called before the first frame update
    void Start()
    {
        cartman = gameObject.GetComponent<CinemachineDollyCart>();
        enginePlumes.SetFloat("hueOffset", 0);
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoosting){
            enginePlumes.SetFloat("hueOffset", hueOffset);
            cartman.m_Speed = boostSpeed;
            targetFOV = boostFOV;
            cs.shakeDuration = 0.1f;
            cs.shakeAmount = 1;
            
        }
        else{
            enginePlumes.SetFloat("hueOffset", 0);
            cartman.m_Speed = mainSpeed;
            targetFOV = normalFOV;
        }

        currentFOV = Mathf.SmoothDamp(currentFOV,targetFOV, ref fovSmoothing, fovZoomTime);
        maincam.fieldOfView = currentFOV;
    }

    

    public void toggleBoost(){
        isBoosting = !isBoosting;
    }
}
