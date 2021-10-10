using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRaycaster : MonoBehaviour
{
    public GameObject waterSpike;
    public float maxScale = 2f;

    public float minScale = 0f;

    public float velocityXSmoothing;
    public float accelerationTime = 0.1f;


    public float maxPos = 5f;

    public float minPos = 1f;

    private Vector3 currentScale;

    public bool isBoosting;
    
    MainPiloting newControls;

    public float boostMultipler = 1.5f;

    public float xRotation = 15f;

    float rotationSmoothing;

    public float rotationAccelerationTime = 0.2f;

    Vector3 waterspikeRot;

    public float targetYRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        newControls = new MainPiloting();
        newControls.Enable();
    }

    float map(float s, float a1, float a2, float b1, float b2)
{
    return b1 + (s-a1)*(b2-b1)/(a2-a1);
}

    // Update is called once per frame
    void Update()
    {
        float offsetDistance = maxPos + 1f;
        waterSpike.SetActive(true);
        
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, -Vector3.up, out hit) && hit.transform.tag == "Water") {
            offsetDistance = hit.distance;
            
            float targetScale = Mathf.Lerp(maxScale,minScale, map(offsetDistance, minPos,maxPos, 0f,1f));

        waterSpike.transform.position = hit.point;
        waterSpike.transform.rotation = Quaternion.AngleAxis(transform.rotation.y, Vector3.up);
        currentScale = waterSpike.transform.localScale;
        

        if(isBoosting){
            targetScale *= boostMultipler;
        }
        
        currentScale.y = Mathf.SmoothDamp(currentScale.y, targetScale, ref velocityXSmoothing, accelerationTime);

        if(currentScale.y <= 0.1f){
            waterSpike.SetActive(false);
        }

        waterSpike.transform.localScale = currentScale;

        targetYRotation = xRotation * newControls.Player.Move.ReadValue<Vector2>().x;
        

        waterspikeRot.z = Mathf.SmoothDamp(waterspikeRot.z, targetYRotation, ref rotationSmoothing, rotationAccelerationTime);

        waterSpike.transform.rotation = Quaternion.Euler(waterspikeRot);
            

        
        }else{
            waterSpike.SetActive(false);
        }

        



    }
}
