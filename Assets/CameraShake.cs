using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;

	public Vector3 shiftPosition = Vector3.zero;

	public Vector3 targetPos;

	private Vector3 truPos;

	//---- Smoothing -----
	Vector3 smoothingVal;
	float accelerationTime;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;

		truPos = originalPos;
		targetPos = truPos;
	}

	void Update()
	{

		//truPos = Vector3.SmoothDamp(truPos, targetPos, ref smoothingVal, accelerationTime);
		Vector3 tempPos;

		if(shiftPosition != Vector3.zero){
			tempPos = shiftPosition;
		}
		if (shakeDuration > 0)
		{
			camTransform.localPosition = truPos + (Random.insideUnitSphere + shiftPosition) * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = truPos;
		}
	}
}