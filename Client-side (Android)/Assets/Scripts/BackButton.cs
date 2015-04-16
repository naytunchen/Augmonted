using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("BackButton: Update");
		//Debug.Log("BackButton in scene: " + Application.loadedLevelName);	
		
		// if running on Android
		if (Application.platform == RuntimePlatform.Android)
		{
			// if back button is pressed
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				// if AR scene go back to user scene
				if(Application.loadedLevelName == "battle")
					AppMaster.currentScene = "user";
					AppMaster.Instance ().callHit ();
					Application.LoadLevel("user");
				// if user scene kill the app
				if(Application.loadedLevelName == "user")
					Application.Quit();
				// if login scene kill the app
				if(Application.loadedLevelName == "login")
					Application.Quit();
			}
		}
	}
}
