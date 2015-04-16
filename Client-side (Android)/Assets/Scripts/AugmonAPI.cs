using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Specialized;
using SimpleJSON;

public class AugmonAPI : MonoBehaviour {
	string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/login";
	//NameValueCollection col = new NameValueCollection();

	string loginStatus = "";

	Text tester;

	// Use this for initialization
	void Start () {
		Debug.Log ("api start");
		string my_data = "{\"email\":\"aaa@aaa.com\",\"password\":\"aaa\"}";

		tester = (Text) GameObject.Find ("LoginStatus").GetComponent<Text>();
		
		Hashtable headers = new Hashtable ();
		headers.Add ("Content-Type", "application/json");
		byte[] pData = System.Text.Encoding.UTF8.GetBytes (my_data);
		WWW www = new WWW (url, pData, headers);

		// wait for www to finish downloading
		StartCoroutine (WaitForRequest (www));

		/* save below for HTTP GET maybe
		Debug.Log ("api start");

		// make new form
		WWWForm form = new WWWForm ();

		form.AddField ("email", "aaa@aaa.com");
		form.AddField ("password", "aaa");

		// using this ctor the www is automatically a POST request
		WWW www = new WWW (url, form);

		StartCoroutine (WaitForRequest (www));

		/*
		col ["email"] = "aaa@aaa.com";
		col ["password"] = "aaa";

		using (WebClient client = new WebClient()) {
			byte[] response = client.UploadValues(url, "POST", col);

			string result = System.Text.Encoding.UTF8.GetString(response);

			Debug.Log(result);
			client.Dispose();
		}
		*/
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;

		// check for errors
		if(www.error == null) {
			Debug.Log("WWW OK! - result: " + www.text);
			var json_str = JSON.Parse (www.text);
			Debug.Log (json_str ["success"].Value);
			loginStatus = json_str["success"].Value;
			//loginStatusText.guiText.text = loginStatus;
			tester.text = loginStatus;
		}
		else {
			Debug.Log("WWW Error: " + www.error);	
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
