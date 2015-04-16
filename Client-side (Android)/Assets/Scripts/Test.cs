using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Database db = new Database ();

		DAO dao = new DAO ();
		Debug.Log ("Login With Email:\t\t" + dao.LoginWithEmail ("aaa@aaa.com","aaa"));
		Debug.Log ("Register with Email:\t\t" + dao.Register("zzz@zzz.com","zzz"));
		Debug.Log ("Register with Facebook:\t\t" + dao.RegisterWithFacebook("32151fads12321"));

		Debug.Log ("Login with Facebook:\t\t" + dao.LoginWithFacebook("32151fads12321"));

		Augmon augmon = new Augmon ();
		augmon.ID = 2;
		augmon.Total_xp = 292929;
		augmon.Attack = 25;
		augmon.Defense = 40;
		augmon.Happiness = 88;

		Debug.Log ("Update Augmon Info:\t\t" + dao.UpdateAugmon (augmon));

		Pedometer p = new Pedometer ();
		p.ID = 2;
		p.Total_step = 10231;
		p.Daily_step = 2131;
		Debug.Log ("Update Pedometer Info:\t\t" + dao.UpdatePedometer (p));

		Debug.Log ("Get Augmon Info:\t\t" + dao.GetAugmonInfo (2));

		Debug.Log ("Get Pedometer Info:\t\t" + dao.GetPedometerInfo (2));

		Debug.Log ("Get User Info:\t\t" + dao.GetUserInfo (2));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
