using UnityEngine;
using System.Collections;
using System;

/*
 * Pick which scene to load can specify in here or 
 * in the Unity editor.
 */
public class LoadScene : MonoBehaviour {
	void Awake()
	{
	}

	public void loadScene(string scene) {
		if (scene.Equals ("login")) {
			FacebookManager.Instance().callLogout();
			StartCoroutine ("Logout");
		}

		AppMaster.currentScene = scene;
		AppMaster.Instance ().callHit ();

		Application.LoadLevel (scene);
	}

	IEnumerator CheckForSuccessfulLogout() {
		// keep yielding until no longer logged in
		if(FB.IsLoggedIn) {
			Debug.Log("Logging out - still logged in" + DateTime.Now.ToString("h:mm:ss tt"));
			yield return new WaitForSeconds (0.1f);
			StartCoroutine("CheckForSuccessfulLogout");
		}
		else {
			// successful logout, do what you want here

			//clear facebook instance
			var inst = FacebookManager.Instance();
			inst.FB_ID = null;
			inst.user_ID = -1;
			inst.ProfilePic = null;
			inst.Gender = "";
			inst.FullName = "";
			inst.callLogout ();

			Debug.Log("Logged out!");
			Application.LoadLevel("login"); // logged out, go load the login level now
		}
	}

	IEnumerator Logout() {
		// set the indicator to use
		Handheld.SetActivityIndicatorStyle (AndroidActivityIndicatorStyle.Large);
		Handheld.StartActivityIndicator ();

		Debug.Log ("Logout was pressed");
		// keep spinning until coroutine finishes
		yield return StartCoroutine ("CheckForSuccessfulLogout");
	}
}
