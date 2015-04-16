using System;
using UnityEngine;
using System.Collections;


public class FeaturePedometer : MonoBehaviour
{
	static FeaturePedometer instance = null;
	private float minLimit = 0.005F;
	private float maxLimit = 0.3F;
	public int stepCnt = 0;
	private bool state = false;
	private float ihigh = 16.0F;
	private float currPed= 0F;
	private float ilow = 0.2F;
	private float avgPed;
	
	void Awake() {
		Debug.Log("FeaturePedometer: Awake");
		
		if (instance == null)
			instance = this;
		
		// keep object alive in all scenes
		DontDestroyOnLoad (gameObject);
	}
	
	public void UpdateStep() {
		currPed = Mathf.Lerp (currPed, Input.acceleration.magnitude, Time.deltaTime * ihigh);
		avgPed = Mathf.Lerp (avgPed, Input.acceleration.magnitude, Time.deltaTime * ilow);
		float delta = currPed - avgPed;
		if (!state) { 
			if (delta > maxLimit) {
				state = true;
				stepCnt++;
			} else if (delta < minLimit) {
				state = false;
			}
			state = false;
		}
		avgPed = currPed;
		calculateDistance(stepCnt);

	}
	
	public int calculateDistance(int cnt)
	{
		return cnt;
	}
	
	public static FeaturePedometer Instance() {
		return instance;
	}
}

