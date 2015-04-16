﻿//  Copyright 2014 Invex Games http://invexgames.com
//	Licensed under the Apache License, Version 2.0 (the "License");
//	you may not use this file except in compliance with the License.
//	You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
//	Unless required by applicable law or agreed to in writing, software
//	distributed under the License is distributed on an "AS IS" BASIS,
//	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//	See the License for the specific language governing permissions and
//	limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public static class MaterialUIEditorTools
{
	private const string versionInfo = "v0.2.2";

	static GameObject theThing;
	static GameObject selectedObject;
	static bool notCanvas;

	static void SetupObject (string objectName)
	{
		selectedObject = Selection.activeGameObject;
		theThing.name = objectName;

		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				if (selectedObject.GetComponentInParent<Canvas>())
					notCanvas = false;
				else
					notCanvas = true;
			}
			else
				notCanvas = true;
		}
		else
			notCanvas = true;

		if (notCanvas)
		{
			if (!GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>())
			{
				GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/EventSystem.prefab", typeof(GameObject))).name = "EventSystem";
			}

			if (GameObject.FindObjectOfType<Canvas>())
			{
				selectedObject = GameObject.FindObjectOfType<Canvas>().gameObject as GameObject;
			}
			else
			{
				selectedObject = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Canvas.prefab", typeof(GameObject))) as GameObject;
				selectedObject.name = "Canvas";
			}
		}

		theThing.transform.SetParent(selectedObject.transform);
		theThing.transform.localPosition = Vector3.zero;
		theThing.transform.localScale = new Vector3 (1, 1, 1);
		Selection.activeGameObject = theThing;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Background", false, 1)]
	[MenuItem("MaterialUI/Create/Background", false, 1)]
	static void CreateBackground()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Background.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Background");
		theThing.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Panel", false, 1)]
	[MenuItem("MaterialUI/Create/Panel", false, 1)]
	static void CreatePanel()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Panel.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Panel");
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Button - Flat", false, 2)]
	[MenuItem("MaterialUI/Create/Button - Flat", false, 2)]
	static void CreateButtonFlat()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Button - Flat.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Button - Flat");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Button - Raised", false, 3)]
	[MenuItem("MaterialUI/Create/Button - Raised", false, 3)]
	static void CreateButtonRaised()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Button - Raised.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Button - Raised");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Round Button - Flat", false, 4)]
	[MenuItem("MaterialUI/Create/Round Button - Flat", false, 4)]
	static void CreateRoundButtonFlat()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Round Button - Flat.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Round Button - Flat");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Round Button - Raised", false, 5)]
	[MenuItem("MaterialUI/Create/Round Button - Raised", false, 5)]
	static void CreateRoundButtonRaised()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Round Button - Raised.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Round Button - Raised");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Small Round Button - Flat", false, 6)]
	[MenuItem("MaterialUI/Create/Small Round Button - Flat", false, 6)]
	static void CreateSmallRoundButtonFlat()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Round Button - Small - Flat.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Small Round Button - Flat");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Small Round Button - Raised", false, 7)]
	[MenuItem("MaterialUI/Create/Small Round Button - Raised", false, 7)]
	static void CreateSmallRoundButtonRaised()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Round Button - Small - Raised.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Small Round Button - Raised");
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Spinny Arrow Button", false, 7)]
	[MenuItem("MaterialUI/Create/Spinny Arrow Button", false, 7)]
	static void CreateSpinnyArrowButton()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/SpinnyArrow Button.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Spinny Arrow Button");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Checkbox", false, 8)]
	[MenuItem("MaterialUI/Create/Checkbox", false, 8)]
	static void CreateCheckbox()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Checkbox.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Checkbox");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Radio Buttons", false, 9)]
	[MenuItem("MaterialUI/Create/Radio Buttons", false, 9)]
	static void CreateRadioButtons()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/RadioGroup.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Radio Buttons");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Switch", false, 10)]
	[MenuItem("MaterialUI/Create/Switch", false, 10)]
	static void CreateSwitch()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Switch.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Switch");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Text Input", false, 11)]
	[MenuItem("MaterialUI/Create/Text Input", false, 11)]
	static void CreateTextInput()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/TextInput.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Text Input");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Slider", false, 12)]
	[MenuItem("MaterialUI/Create/Slider", false, 12)]
	static void CreateSlider()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Slider.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Slider");
	}
	
	[MenuItem("GameObject/Create Other/MaterialUI/Selection Box", false, 13)]
	[MenuItem("MaterialUI/Create/Selection Box", false, 13)]
	static void CreateSelectionBox()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/SelectionBox.prefab", typeof(GameObject))) as GameObject;
		SetupObject ("Selection Box");
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Selection Box - Flat", false, 13)]
	[MenuItem("MaterialUI/Create/Selection Box - Flat", false, 13)]
	static void CreateSelectionBoxFlat()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/SelectionBox - Flat.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Selection Box - Flat");
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Dialog Box - Normal", false, 13)]
	[MenuItem("MaterialUI/Create/Dialog Box - Normal", false, 13)]
	static void CreateDialogBoxNormal()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/DialogBox - Normal.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Dialog Box - Normal");
		theThing.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Dialog Box - Scroll", false, 13)]
	[MenuItem("MaterialUI/Create/Dialog Box - Scroll", false, 13)]
	static void CreateDialogBoxScroll()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/DialogBox - Scroll.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Dialog Box - Scroll");
		theThing.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Dialog Box - Simple", false, 13)]
	[MenuItem("MaterialUI/Create/Dialog Box - Simple", false, 13)]
	static void CreateDialogBoxSimple()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/DialogBox - Simple.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Dialog Box - Simple");
		theThing.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Nav Drawer", false, 13)]
	[MenuItem("MaterialUI/Create/Nav Drawer", false, 13)]
	static void CreateNavDrawer()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Nav Drawer.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Nav Drawer");
		theThing.GetComponent<RectTransform>().sizeDelta = new Vector2(theThing.GetComponent<RectTransform>().sizeDelta.x, 8f);
		theThing.GetComponent<RectTransform>().anchoredPosition = new Vector2(-theThing.GetComponent<RectTransform>().sizeDelta.x / 2f, 0f);
	}

	[MenuItem("GameObject/Create Other/MaterialUI/App Bar", false, 13)]
	[MenuItem("MaterialUI/Create/App Bar", false, 13)]
	static void CreateAppBar()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/App Bar.prefab", typeof(GameObject))) as GameObject;
		SetupObject("App Bar");
		theThing.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		theThing.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	[MenuItem("GameObject/Create Other/MaterialUI/Screen", false, 13)]
	[MenuItem("MaterialUI/Create/Screen", false, 13)]
	static void CreateScreen()
	{
		theThing = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/MaterialUI/ComponentPrefabs/Screen.prefab", typeof(GameObject))) as GameObject;
		SetupObject("Screen");
		theThing.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		theThing.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	[MenuItem("Component/MaterialUI/Ripple Config")]
	[MenuItem("MaterialUI/Add Component/Ripple Config")]
	static void AddRippleConfig()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("RippleConfig");
			}
		}
	}

	[MenuItem("Component/MaterialUI/Shadow Config")]
	[MenuItem("MaterialUI/Add Component/Shadow Config")]
	static void AddShadowConfig()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("ShadowConfig");
			}
		}
	}

	[MenuItem("Component/MaterialUI/Rect Transform Snapper")]
	[MenuItem("MaterialUI/Add Component/Rect Transform Snapper")]
	static void AddRectTransformSnap()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("RectTransformSnap");
			}
		}
	}

	[MenuItem("Component/MaterialUI/Shadow Generator")]
	[MenuItem("MaterialUI/Add Component/Shadow Generator")]
	static void AddShadowGen()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("ShadowGen");
			}
		}
	}

	[MenuItem("Component/MaterialUI/Toaster")]
	[MenuItem("MaterialUI/Add Component/Toaster")]
	static void AddToaster()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("Toaster");
			}
		}
	}
	
	[MenuItem("Component/MaterialUI/EZAnim")]
	[MenuItem("MaterialUI/Add Component/EZAnim")]
	static void AddEZAnim()
	{
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject)
		{
			if (GameObject.Find(selectedObject.name))
			{
				selectedObject.AddComponent("EZAnim");
			}
		}
	}

	[MenuItem("MaterialUI/Report a Bug - Request a Feature")]
	static void Feedback()
	{
		Application.OpenURL("https://github.com/InvexGames/MaterialUI/issues");
	}

	[MenuItem("MaterialUI/Wiki")]
	static void Wiki()
	{
		Application.OpenURL("https://github.com/InvexGames/MaterialUI/wiki");
	}

	[MenuItem("MaterialUI/Check for Update (current " + versionInfo + ")")]
	static void About()
	{
		Application.OpenURL("https://github.com/InvexGames/MaterialUI/releases");
	}
}
