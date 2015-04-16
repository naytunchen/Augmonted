using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MaterialUI;
using System.Net.Mail;
using System;
using System.Text.RegularExpressions;

/*
 * This class handles logging in the user into the app.
 * If the user is already logged in via Facebook then
 * lead them to the app. Otherwise have the user
 * log in via Facebook.
 */
public class Login : MonoBehaviour {

	void Awake()
	{
		Debug.Log("Login: Awake");
		Debug.Log("FB.IsLoggedIn value: " + FacebookManager.Instance().IsLogged());
		if (FacebookManager.Instance ().IsLogged ())
			Application.LoadLevel ("user");
	}
	
	// Update is called once per frame
	void Update () {
		if (FacebookManager.Instance ().IsLogged ())
			Application.LoadLevel ("user");
		else {
			// handle errors here
		}
	}

	public void CallFBLogin() {
		AppMaster.currentScene = "user";
		AppMaster.Instance ().callHit ();
		FacebookManager.Instance ().callLogin ();
	}
	//pattern matching
	public bool IsValidEmailAddress(string mailAddress)
	{
		Regex mailIDPattern = new Regex(@"[\w-]+@([\w-]+\.)+[\w-]+");
		
		if (!string.IsNullOrEmpty(mailAddress) && mailIDPattern.IsMatch(mailAddress))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void CallLogin() {
		try{
			InputField[] arr = UnityEngine.Object.FindObjectsOfType<InputField>();
			string username = arr[0].text;
			string password = arr[1].text;

			if(!IsValidEmailAddress(username)){
				throw new Exception("E-mail is in wrong format");
			}
			
			DAO database = new DAO();
			database.Register (username, password);

			FacebookManager.Instance ().user_ID = database.LoginWithEmail (username,password);
			if(FacebookManager.Instance ().user_ID > -1){
				Texture2D tex = Resources.Load("profile") as Texture2D;
				FacebookManager.Instance ().ProfilePic = Sprite.Create(tex
				                                                       , new Rect(0, 0, 128, 128), new Vector2(0, 0));


				AppMaster.currentScene = "user";
				AppMaster.Instance ().callHit ();
				Application.LoadLevel ("user");
			} else {
				Debug.Log("Failed Login");
			}
			
		} catch (Exception ex){
			Debug.Log ("Error : " + ex.Message);
		}
	}


}
