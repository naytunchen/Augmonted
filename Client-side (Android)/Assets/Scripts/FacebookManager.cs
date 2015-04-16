using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;

/*
 * This class is the wrapper for the Facebook API.
 * The single instance is use to call the API methods.
 * To call FB APIs simply do FacebookManager.Instance().MethodName()
 * 
 * DO NOT use Application.LoadLevel in here. Leave that up to
 * scripts local to the scene.
 */
public class FacebookManager : MonoBehaviour {

	static FacebookManager instance = null;

	public string FullName;
	public string Gender;
	public Sprite ProfilePic;
	public string FB_ID;
	public int user_ID;
	
	private Dictionary<string, string> profile;
	
	//string meQueryString = "/v2.0/me?fields=id, name, first_name";
	string meQueryString = "/v2.0/me?fields=name";
	string idQueryString = "/v2.0/me?fields=id";
	Dictionary<string, string> formData;
	

	public static FacebookManager Instance() {
		return instance;
	}

	// Use this for initialization
	void Awake () {
		Debug.Log("FacebookManager: Awake");
		
		if (instance == null)
			instance = this;
		
		// Initialize FB SDK
		FB.Init (onInitCallback, onHideUnityCallback);
		DontDestroyOnLoad (gameObject);
	}

	public void callInit() {
		FB.Init (onInitCallback, onHideUnityCallback);
	}

	public void callLogin() {
		/*
		 * public_profile gives us access to these fields
		 * id
		 * name
		 * first_name
		 * last_name
		 * link
		 * gender
		 * locale
		 * timezone
		 * updated_time
		 * verified
		 */
		FB.Login ("public_profile, email", onLoginCallback);
		DAO database = new DAO ();
		database.RegisterWithFacebook (FB_ID);
		user_ID = database.LoginWithFacebook(FB_ID);
	}

	public void callLogout() {
		FB.Logout ();
	}

	public bool IsLogged() {
		return FB.IsLoggedIn;
	}

	private void onInitCallback() {
		Debug.Log ("onInitCallback");
		
		Debug.Log("FB.IsLoggedIn value: " + FB.IsLoggedIn);
		if(FB.IsLoggedIn) {
			Debug.Log("Already logged in");
			onLoggedIn();
		}
		else {
			Debug.Log ("Not logged in");
		}
	}
	
	private void onLoggedIn() {
		Debug.Log("Logged in. ID: " + FB.UserId);
	
		// get profile picture
		FB.API (Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, onPictureCallback);
		// get name
		FB.API (meQueryString, Facebook.HttpMethod.GET, onNameCallback);
		// get ID
		FB.API (idQueryString, Facebook.HttpMethod.GET, onIDCallback);
	}

	private void onHideUnityCallback(bool isGameShown) {
		if (!isGameShown)
			Time.timeScale = 0; // pause game
		else
			Time.timeScale = 1; // resume game
	}

	private void onLoginCallback(FBResult result) {
		if (FB.IsLoggedIn) {
			Debug.Log ("FB Login Worked");
			onLoggedIn();
		}
		else
			Debug.Log ("FB Login Failed");
	}

	private void onPictureCallback(FBResult result) {

		if (result.Error != null) {
			Debug.Log ("Could not get profile picture");
			
			// try again to get profile picture
			FB.API (Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, onPictureCallback);
			return;
		} 

		ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
		Debug.Log("Received profile picture");
	}

	private void onNameCallback(FBResult result) {
		
		if (result.Error != null) {
			Debug.Log ("Could not get a name");
			
			Debug.Log(result.Error);
			
			// try again to get name
			FB.API (meQueryString, Facebook.HttpMethod.GET, onNameCallback);
			return;
		} 

		// to get access to other json fields must update Util.cs to do so
		profile = Util.DeserializeJSONProfile(result.Text);
		//FullName = profile["first_name"];
		FullName = profile["name"];
		Debug.Log("Name is: " + FullName);
	}
	
	private void onIDCallback(FBResult result) {
		
		if (result.Error != null) {
			Debug.Log ("Could not get ID");
			
			Debug.Log(result.Error);
			
			// try again to get name
			FB.API (idQueryString, Facebook.HttpMethod.GET, onIDCallback);
			return;
		} 
		
		// to get access to other json fields must update Util.cs to do so
		profile = Util.DeserializeJSONProfile(result.Text);
		FB_ID = profile["id"];
		Debug.Log("ID is: " + FB_ID);
	}
}
