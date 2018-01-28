using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour {
	public RectTransform canvases;
	public Camera cam;
	
	private float duration;
	private float intensity;
	private float attenuation;
	private float shakeAngle;
	
	public void ShakeScreen(float _duration = 0.1f, float _intensity = 0.7f, float _attenuation = 1.0f) 
	{
		duration = _duration;
		intensity = _intensity;
		attenuation = _attenuation;
		shakeAngle = 0;
		// shake canvases by offsetting their positions
		// shake camera
		StartCoroutine(ShakingScreen());
	}
	
	public void Update() 
	{
		//if (Input.GetKey("space")) // GetKeyDown
        //    ShakeScreen(5f, 0.4f, 1f);
	}
	
	IEnumerator ShakingScreen() {
		if (duration > 0) {
			cam.transform.localPosition = Random.insideUnitSphere * intensity;
			shakeAngle = (shakeAngle + Mathf.PI * 0.7f) % (Mathf.PI * 2f);
			cam.transform.rotation *= Quaternion.Euler(
				Mathf.Cos(shakeAngle) * duration, 
				Mathf.Sin(shakeAngle) * duration, 
				0f
			);
			duration -= Time.deltaTime * attenuation;
			// TODO: tween return to center
		} else {
			duration = 0f;
		}
		yield return new WaitForEndOfFrame();
	}
}
