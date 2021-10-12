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

    public ParticleSystem engineBack;

    public waterRaycaster mistManager;

   //--------------------------------

    public Material boostParticles;
     [ColorUsage(true, true)]
    public Color defaultcolor;
    
     [ColorUsage(true, true)]
    public Color boostColor;
    public Material vaporCone;

    public float lengthToVisible = 0.5f;

    private float curLength = 0f;

    private bool hasTriggered = false;

    public ParticleSystem speedLines;
    // Start is called before the first frame update
    void Start()
    {
        cartman = gameObject.GetComponent<CinemachineDollyCart>();
        enginePlumes.SetFloat("hueOffset", 0);
        maincam = Camera.main;
        vaporCone.SetFloat("alphaMultiplier", 0);
    }

    // Update is called once per frame
    void Update()
    {
        var main = engineBack.main;
        var col = engineBack.colorOverLifetime;
        if(isBoosting){
            enginePlumes.SetFloat("hueOffset", hueOffset);
            cartman.m_Speed = boostSpeed;
            targetFOV = boostFOV;
            cs.shakeDuration = 0.1f;
            cs.shakeAmount = 0.5f;
            mistManager.isBoosting= true;
            if( hasTriggered == false){
                curLength = lengthToVisible;
                 hasTriggered = true;
                 var emission = speedLines.emission;
                 emission.enabled = true;
            }

            curLength -= Time.deltaTime;

            if(curLength > 0){
                vaporCone.SetFloat("alphaMultiplier", 1);
                cs.shakeDuration = 0.3f;
                cs.shakeAmount = 5.5f;
            }
            else{
                if(vaporCone.GetFloat("alphaMultiplier") > 0f){
                    vaporCone.SetFloat("alphaMultiplier", vaporCone.GetFloat("alphaMultiplier") - (15f * Time.deltaTime));
                }
                else{
                    vaporCone.SetFloat("alphaMultiplier",0f);
                }
                
            }

            boostParticles.SetColor("_EmissionColor", boostColor);

        }
        else{
            enginePlumes.SetFloat("hueOffset", 0);
            cartman.m_Speed = mainSpeed;
            targetFOV = normalFOV;
            mistManager.isBoosting= false;
             hasTriggered = false;
            
            if(vaporCone.GetFloat("alphaMultiplier") > 0f){
                    vaporCone.SetFloat("alphaMultiplier", vaporCone.GetFloat("alphaMultiplier") - (15f * Time.deltaTime));
            }
            else{
                vaporCone.SetFloat("alphaMultiplier",0f);
            }
            boostParticles.SetColor("_EmissionColor", defaultcolor);

             var emission = speedLines.emission;
            emission.enabled = false;
        }

        currentFOV = Mathf.SmoothDamp(currentFOV,targetFOV, ref fovSmoothing, fovZoomTime);
        maincam.fieldOfView = currentFOV;

        
    }

    

    public void toggleBoost(){
        isBoosting = !isBoosting;
    }
}
