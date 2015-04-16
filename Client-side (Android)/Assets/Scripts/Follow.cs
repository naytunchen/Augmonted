using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	
	private Transform target;
	private Vector3 targetRotation;
	
	//----------------------------------------------------------------------
	void Start()
	{
		target = GameObject.Find ("ARCamera").transform;
	}
	
	//----------------------------------------------------------------------
	void Update()
	{
		targetRotation = target.transform.eulerAngles;
		transform.eulerAngles = targetRotation;
	}
}
