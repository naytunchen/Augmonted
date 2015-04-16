using UnityEngine;
using System.Collections;

/*
 * Singleton object for each scene. Should be useful
 * for reusable methods we want.
 * e.g. the back button
 */

public class AppMaster : MonoBehaviour {

	public static string currentScene = "splash";

	public static AppMaster instance = null;

	public GoogleAnalyticsV3 googleAnalytics;

	// access this anywhere using AppMaster.AM.name
	public string name = "augmon";

	public static AppMaster Instance() {
		return instance;
	}

	void Awake() {
		Debug.Log("AppMaster: Awake");
		
		if (instance == null) {
						instance = this;
		}
		if (googleAnalytics != null) {
			googleAnalytics.LogScreen (currentScene);
		}

		// keep the gameObject otherwise this
		// instance won't exist
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (googleAnalytics);
	}

	// Use this for initialization
	void Start () {
		if (googleAnalytics != null) {
			googleAnalytics.LogScreen (currentScene);
		}
	}

	public void callHit() {
		Debug.Log ("Called Hit On: " + currentScene);
		if (googleAnalytics != null) {
			googleAnalytics.LogScreen (currentScene);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("AppMaster: Update");
		Debug.Log ("AppMaster in scene: " + Application.loadedLevelName);

		//if (googleAnalytics != null) {
		//	googleAnalytics.LogScreen (currentScene);
		//}

		// Couldn't use back button logic here because the update function here doesn't
		// get called in other scenes for some reason
		// refer to _BackButtonManager GameObject and BackButton.cs to handle back button press
	}
}
