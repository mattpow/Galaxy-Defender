using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class StartScreen : MonoBehaviour
{

	public Text highScoreText;
	// Use this for initialization
	void Start ()
	{
		highScoreText.text = "High Score " + PlayerPrefs.GetInt ("highScore").ToString ();
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate ((bool success) => { });
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayGamesPlatform.Instance.IsAuthenticated () == false) {
			Social.localUser.Authenticate ((bool success) => { });
		}
	}
}
