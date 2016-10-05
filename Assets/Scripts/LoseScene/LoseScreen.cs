using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;
using ChartboostSDK;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LoseScreen : MonoBehaviour
{
	public Text roundScore;
	public Text highScore;
	public bool showAd;
	public static bool hasDisplayed = false;
	private int ItemBalance;

				
	// Use this for initialization
	void Start ()
	{
		hasDisplayed = false;
		roundScore.text = "Score " + Score.currentScore.ToString ();
		highScore.text = "High Score: " + PlayerPrefs.GetInt ("highScore").ToString ();
		//Chartboost.Create ();
		
		Chartboost.setAutoCacheAds (true);		
		
		if (!SoomlaStore.Initialized) {
			SoomlaStore.Initialize (new Soomla.Store.GalaxyDefender.GalaxyDefenderAssets ());
			SoomlaStore.RefreshInventory ();
		}
		
		ItemBalance = StoreInventory.GetItemBalance ("no_ads");
		
		if (ItemBalance > 0) {
			showAd = false;
		} else {
			showAd = true;
		}
		
		if (Score.currentScore >= 10000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQAQ", 100.0f, (bool success) => { });
		}	
		if (Score.currentScore >= 100000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQAg", 100.0f, (bool success) => { });
		}	
		if (Score.currentScore >= 250000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQAw", 100.0f, (bool success) => { });
		}	
		if (Score.currentScore >= 500000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQBA", 100.0f, (bool success) => { });
		}	
		if (Score.currentScore >= 1000000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQBQ", 100.0f, (bool success) => { });
		}	
		if (Score.currentScore >= 5000000) {
			Social.ReportProgress ("CgkIl_fbqZ4MEAIQBw", 100.0f, (bool success) => { });
		}
		
		Social.ReportScore (Score.currentScore, "CgkIl_fbqZ4MEAIQBg", (bool success) => { });	
	}
	
	// Update is called once per frame
	void Update ()
	{ 	
		ItemBalance = StoreInventory.GetItemBalance ("no_ads");
		
		if (ItemBalance > 0) {
			showAd = false;
		} else {
			showAd = true;
		}
		if (showAd == true && hasDisplayed == false) {
			Chartboost.showInterstitial (CBLocation.locationFromName ("Lose"));
			hasDisplayed = true;
		}
	}
	
	public void RemoveAds ()
	{
		StoreInventory.BuyItem ("no_ads");
	}
	
	public void showLeaderboard ()
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI ("CgkIl_fbqZ4MEAIQBg");
	}
	
	public void showAchievement ()
	{
		Social.ShowAchievementsUI ();
	}
	
}
