using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;

public class DAO {


	public DAO()
	{
	}

	public bool Register(string email, string password)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/register";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"email\":\"" + email + "\", \"first_name\":\"\", \"last_name\":\"\", \"password\":\"" + password + "\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";

			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();

			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();

			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();

			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());

			var success = N ["success"];

			if(success.AsBool)
			{
				result = true;
			}

		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}

		return result;
	}

	public bool RegisterWithFacebook(string facebook_id)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/facebookRegister";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"facebook_id\":\""+facebook_id+"\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			
			if(success.AsBool)
			{
				result = true;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}

		return result;
	}

	public int LoginWithFacebook(string facebook_id)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/facebookLogin";
		string jsonString;
		int result = -1;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"facebook_id\":\""+facebook_id+"\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			var id = N["user_id"];
			
			if(success.AsBool)
			{
				result = id.AsInt;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return result;
	}

	public int LoginWithEmail(string email, string password)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/login";
		int result = -1;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"email\":\""+ email +"\",\"password\":\""+ password +"\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			string jsonString = sr.ReadToEnd ();
			Debug.Log ("jsonString:\t" + jsonString);
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			
			var success = N ["success"];
			var id = N["user_id"];
			
			if(success.AsBool)
			{
				result = id.AsInt;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}

		return result;
	}

	public User GetUserInfo(int id)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/"+id;
		User user = new User ();
		try
		{
			HttpWebRequest http = (HttpWebRequest)WebRequest.Create (url);
			HttpWebResponse response = (HttpWebResponse)http.GetResponse();
			Stream stream = response.GetResponseStream();
			StreamReader sr = new StreamReader(stream);
			
			string jsonString = sr.ReadToEnd ();
			Debug.Log ("jsonString:\t" + jsonString);
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse(jsonString);
			Debug.Log ("string:\t" + N);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N["success"];
			Debug.Log ("success:\t" + success.AsBool);
			
			if(success.AsBool)
			{
				user.ID = N["data"]["id"].AsInt;
				user.Facebook_ID = N["data"]["facebook_id"];
				user.Email = N["data"]["email"];
				user.FirstName = N["data"]["first_name"];
				user.LastName = N["data"]["last_name"];
				
				user.AugmonInfo = new Augmon();
				user.PedometerInfo = new Pedometer();
				
				user.AugmonInfo.Name = N["data"]["name"];
				user.AugmonInfo.Total_xp = N["data"]["total_xp"].AsInt;
				user.AugmonInfo.Lvl = N["data"]["lvl"].AsInt;
				user.AugmonInfo.Attack = N["data"]["attack"].AsInt;
				user.AugmonInfo.Defense = N["data"]["defense"].AsInt;
				user.AugmonInfo.Happiness = N["data"]["happiness"].AsInt;
				
				user.PedometerInfo.Total_step = N["data"]["total_steps"].AsInt;
				user.PedometerInfo.Daily_step = N["data"]["daily_steps"].AsInt;
				
				
				
				Debug.Log ("User:\t\t" + "ID : " + user.ID + "\tfacebook_id : " + user.Facebook_ID + "\tfirst_name : " + user.FirstName + "\tlast_name : " + user.LastName +"\n");
				Debug.Log ("User's Augmon: Total_xp : " +  user.AugmonInfo.Total_xp + "\tLvl : " + user.AugmonInfo.Lvl +"\tAttack : " +user.AugmonInfo.Attack +"\tDefense : " + user.AugmonInfo.Defense + "\tHappiness : " + user.AugmonInfo.Happiness +"\n");
				Debug.Log ("User's Pedometer: Total_steps : " + user.PedometerInfo.Total_step + "\tDaily_steps : " +user.PedometerInfo.Daily_step + "\n");
				
				return user;
			}
			
			
		}
		catch(Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return null;
	}

	public Augmon GetAugmonInfo(int id)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/augmon/"+id.ToString ();
		Augmon augmon = new Augmon ();
		try
		{
			HttpWebRequest http = (HttpWebRequest)WebRequest.Create (url);
			HttpWebResponse response = (HttpWebResponse)http.GetResponse();
			Stream stream = response.GetResponseStream();
			StreamReader sr = new StreamReader(stream);
			
			string jsonString = sr.ReadToEnd ();
			Debug.Log ("jsonString:\t" + jsonString);
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse(jsonString);
			Debug.Log ("string:\t" + N);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N["success"];
			Debug.Log ("success:\t" + success.AsBool);
			
			if(success.AsBool)
			{
				augmon.ID = N["data"]["id"].AsInt;
				augmon.Name = N["data"]["name"];
				augmon.Total_xp = N["data"]["total_xp"].AsInt;
				augmon.Lvl = N["data"]["lvl"].AsInt;
				augmon.Attack = N["data"]["attack"].AsInt;
				augmon.Defense = N["data"]["defense"].AsInt;
				augmon.Happiness = N["data"]["happiness"].AsInt;
				Debug.Log ("Augmon:\t\t" + "ID : " + augmon.ID + "\tTotal_xp : " + augmon.Total_xp + "\tLvl : " + augmon.Lvl + "\tattack : " + augmon.Attack + "\tdefense : " + augmon.Defense + "\thappiness : " + augmon.Happiness);
				return augmon;
			}
			
			
		}
		catch(Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return null;
	}

	public Pedometer GetPedometerInfo(int id)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/pedometer/"+id.ToString();
		Pedometer p = new Pedometer ();
		try
		{
			HttpWebRequest http = (HttpWebRequest)WebRequest.Create (url);
			HttpWebResponse response = (HttpWebResponse)http.GetResponse();
			Stream stream = response.GetResponseStream();
			StreamReader sr = new StreamReader(stream);
			
			string jsonString = sr.ReadToEnd ();
			Debug.Log ("jsonString:\t" + jsonString);
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse(jsonString);
			Debug.Log ("string:\t" + N);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N["success"];
			Debug.Log ("success:\t" + success.AsBool);
			
			if(success.AsBool)
			{
				p.ID = N["data"]["id"].AsInt;
				p.Total_step = N["data"]["total_steps"].AsInt;
				p.Daily_step = N["data"]["daily_steps"].AsInt;
				Debug.Log ("Pedometer:\t\t" + "ID : " + p.ID + "\tTotal_steps : " + p.Total_step + "\tDaily_step : " + p.Daily_step );
				return p;
			}
			
			
		}
		catch(Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return null;
	}


	public bool UpdateUser(User user, string password)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/user/update";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"user_id\":"+user.ID+", \"email\":\""+user.Email +"\",\"first_name\":\""+ user.FirstName +"\", \"last_name\":\""+ user.LastName+"\", \"password\":\""+ password +"\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			
			if(success.AsBool)
			{
				result = true;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return result;
	}

	public bool UpdateAugmon(Augmon augmon)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/augmon/update";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"augmon_id\":"+augmon.ID+", \"name\":" + "\""+augmon.Name+"\""+ "\"total_xp\":"+ augmon.Total_xp +",\"attack\":"+ augmon.Attack +", \"defense\":"+ augmon.Defense+", \"happiness\":"+ augmon.Happiness +"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			
			if(success.AsBool)
			{
				result = true;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return result;
	}

	public bool UpdateAugmonName(int id, string name)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/augmon/updateName";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"augmon_id\":"+id+", \"name\":" + "\""+name+"\"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			
			if(success.AsBool)
			{
				result = true;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return result;
	}

	public bool UpdatePedometer(Pedometer pedometer)
	{
		string url = "http://ec2-54-183-168-17.us-west-1.compute.amazonaws.com/augmonted/pedometer/update";
		string jsonString;
		bool result = false;
		try
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			string postData = "{\"p_id\":"+pedometer.ID+", \"total_steps\":"+ pedometer.Total_step +", \"daily_steps\":"+ pedometer.Daily_step +"}";
			byte[] data = encoding.GetBytes (postData);
			var http = (HttpWebRequest)WebRequest.Create (new Uri (url));
			http.Accept = "applicaiton/json";
			http.ContentType = "application/json";
			http.Method = "POST";
			
			Stream stream = http.GetRequestStream ();
			stream.Write (data, 0, data.Length);
			stream.Close ();
			
			WebResponse response = http.GetResponse ();
			stream = response.GetResponseStream ();
			
			StreamReader sr = new StreamReader (stream);
			jsonString = sr.ReadToEnd ();
			
			sr.Close ();
			stream.Close ();
			
			var N = JSON.Parse (jsonString);
			Debug.Log ("Reassembled: " + N.ToString ());
			
			var success = N ["success"];
			
			if(success.AsBool)
			{
				result = true;
			}
			
		}
		catch (Exception ex)
		{
			Debug.Log ("Error : " + ex.Message);
		}
		return result;
	}

}
