using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundFXPlayer : MonoBehaviour
{
	private Button SFXToggle; //SFX toggle button on start scene
	public Sprite SFXOn; //SFX on sprite
	public Sprite SFXOff; //SFX off sprite
	private int SFXPref; //SFX preferences
	
	///static int instance = 0;
	// Use this for initialization
	
	void Awake ()
	{
		if (!PlayerPrefs.HasKey ("allowSFXToPlay")) { //If no SFX playing preference is set, then play SFX by default
			PlayerPrefs.SetInt ("allowSFXToPlay", 1);
		} 	 
	}
	
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Application.loadedLevelName == "Start") {
			if (SFXToggle == null) {
				SFXToggle = GameObject.Find ("Canvas/Sound FX/Sound FX Toggle").GetComponent<Button> ();
				SFXToggle.onClick.AddListener (() => ToggleSFX ());
			}
			SFXPref = PlayerPrefs.GetInt ("allowSFXToPlay"); //Determines whether or not SFX is allowed to play
			if (SFXPref == 1) { // if so
				SFXToggle.GetComponent<Image> ().sprite = SFXOn; //Render the SFX off sprite
			} else {
				SFXToggle.GetComponent<Image> ().sprite = SFXOff; //Render the SFX off sprite
			}
		} 
	}
	
	public void ToggleSFX () //When SFX toggle button is clicked
	{
		if (SFXPref == 1) { //If the SFX preference was previously true
			PlayerPrefs.SetInt ("allowSFXToPlay", 0); //set it to zero/false
			SFXToggle.GetComponent<Image> ().sprite = SFXOff; //Render SFX off sprite
			
		} else if (SFXPref == 0) { //If the SFX preferebce was previously false
			PlayerPrefs.SetInt ("allowSFXToPlay", 1); //Set it to 1/true
			SFXToggle.GetComponent<Image> ().sprite = SFXOn; //Render SFX on sprite
		}
	}
}
