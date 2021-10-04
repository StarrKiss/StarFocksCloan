using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public  GameObject target;

    private Vector3 offset;

    Vector3 posSmoothing;

    public float posTime;
    void Start()
    {
        offset = gameObject.transform.localPosition - target.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition =  Vector3.SmoothDamp(transform.localPosition, (target.transform.localPosition/2) + offset, ref posSmoothing, posTime);
    }
}
