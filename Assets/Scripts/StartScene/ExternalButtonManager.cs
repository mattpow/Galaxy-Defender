using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ExternalButtonManager : MonoBehaviour
{

	public void RateLink ()
	{
		Application.OpenURL ("http://play.google.com/store/apps/details?id=xyz.mattpowell.galaxydefender");
		//Application.OpenURL ("market://details?id=com.matt-powell.galaxy-defender");

	}
	
	public void showLeaderboard ()
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI ("CgkIl_fbqZ4MEAIQBg");
	}
}
