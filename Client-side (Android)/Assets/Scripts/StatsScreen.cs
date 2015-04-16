using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MaterialUI;
using System.Collections.Generic;
using System;

public class StatsScreen : MonoBehaviour {
	private NavDrawerConfig navdrawer;
	private List<ButtonContainer> augmonbuttons;
	private bool statsMenuOn = false;
	private Image icon;
	private Text ButtonText;
	private DAO database;
	enum index {Battle,Stats,Feed,Steps,Logout};

	public class ButtonContainer{
		public string name;
		public Text ButtonText;
		public RippleConfig RippleScript;
		public Image Icon;
		public Button ButtonScript;
	}

	public IEnumerator pause(){
		yield return new WaitForSeconds(1);
		navdrawer.Open ();
		yield return new WaitForSeconds(1);
		statsMenuOn = true;
		yield break;
	}
	public void statsonClick() {
		augmonbuttons = new List<ButtonContainer> ();

		//get and close navdrawer
		navdrawer = GameObject.Find ("Nav Drawer").GetComponent<NavDrawerConfig> ();
		navdrawer.Close ();

		//declare database
		database = new DAO ();

		getStats ();
		//pause to avoid stain
		Invoke("displayStats",1);

		StartCoroutine (pause ());

	}

	//get components
	private void getStats(){
		ButtonContainer container;
		GameObject[] arr = GameObject.FindGameObjectsWithTag ("ButtonLayer");
		for(int i = 0;i<5;i++){
			container = new ButtonContainer();
			container.name =  arr[i].name;
			container.ButtonText = arr[i].GetComponentInChildren<Text>();
			container.RippleScript = arr[i].GetComponent<RippleConfig>();
			container.ButtonScript = arr[i].GetComponent<Button>();
			//stats is a special case, InkBlot is also an image object
			//perhaps change to better method? GetComponentsInChildren() may not
			//be the best idea in this case
			if(container.name == "Stats"){
				container.Icon = arr[i].GetComponentsInChildren<Image>()[2];
			}else{
				container.Icon = arr[i].GetComponentsInChildren<Image>()[1];
			}
			augmonbuttons.Add (container);
		}

	}

	private void displayStats() {
		//change this to FB login ID
		Augmon augmon = database.GetAugmonInfo(FacebookManager.Instance ().user_ID);
		for (int i = 0; i<5; i++) {
			RectTransform trans = augmonbuttons[i].ButtonText.rectTransform;
			augmonbuttons[i].ButtonText.fontStyle = FontStyle.Bold;
			switch (augmonbuttons[i].name){
				case "Battle":
					augmonbuttons[i].ButtonText.text = "Name: " + augmon.Name;
					trans.anchoredPosition = new Vector2(-20,0);
					break;
				case "Stats":
					augmonbuttons[i].ButtonText.text = "Level: " + augmon.Lvl;
					trans.anchoredPosition = new Vector2(-20,0);
					break;
				case "Feed":
					augmonbuttons[i].ButtonText.text = "Attack: " + augmon.Attack;
					trans.anchoredPosition = new Vector2(-20,0);
					break;
				case "Steps":
					augmonbuttons[i].ButtonText.text = "Def: " + augmon.Defense;
					augmonbuttons[i].ButtonText.text = "Health: " + augmon.Happiness + "%";
					trans.anchoredPosition = new Vector2(-20,0);
					break;
				case "Logout":
					augmonbuttons[i].ButtonText.text = "Health: " + augmon.Happiness + "%";
					//augmonbuttons[i].ButtonText.text = "ID: " + augmon.ID;
					trans.anchoredPosition = new Vector2(-20,0);
					break;
			}
			augmonbuttons[i].RippleScript.enabled = false;
			augmonbuttons[i].Icon.enabled = false;
			augmonbuttons[i].ButtonScript.enabled = false;
		}
	}

	private void redisplayStats(){
		for (int i = 0; i<5; i++) {
			augmonbuttons[i].ButtonText.rectTransform.anchoredPosition = new Vector2(28,0);
			augmonbuttons[i].ButtonText.fontStyle = FontStyle.Normal;
			switch (augmonbuttons[i].name){
			case "Battle":
				augmonbuttons[i].ButtonText.text = "Battle";
				break;
			case "Stats":
				augmonbuttons[i].ButtonText.text = "Stats";
				break;
			case "Feed":
				augmonbuttons[i].ButtonText.text = "Feed";
				break;
			case "Steps":
				augmonbuttons[i].ButtonText.text = "Steps";
				break;
			case "Logout":
				augmonbuttons[i].ButtonText.text = "Logout";
				break;
			}
			augmonbuttons[i].RippleScript.enabled = true;
			augmonbuttons[i].Icon.enabled = true;
			augmonbuttons[i].ButtonScript.enabled = true;
		}
	}
	
	private void Update(){
		if(navdrawer != null) {
			if (navdrawer.isClosed() && statsMenuOn) {
				statsMenuOn = false;
				redisplayStats();
			}
		}
	}
}
