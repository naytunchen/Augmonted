using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	private RawImage raw;

	void Awake ()
	{
		raw = GameObject.Find ("Screen Fader").GetComponent<RawImage>();
		// Set the texture so that it is the the size of the screen and covers it.
		raw.uvRect.Set (0f, 0f, Screen.width, Screen.height);
	}
	
	
	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		raw.color = Color.Lerp(raw.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		raw.color = Color.Lerp(raw.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(raw.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			raw.color = Color.clear;
			raw.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		raw.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(raw.color.a >= 0.95f)
			// ... reload the level.
			Application.LoadLevel(0);
	}
}
