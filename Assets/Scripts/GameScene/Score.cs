using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	
	public static int currentScore; //current score
	private int highScore; //high score
	
	public Text scoreText; //Text for current score
	public Text highScoreText; //Text for high score
	
	// Use this for initialization
	void Start ()
	{
		currentScore = 0; 
		if (!PlayerPrefs.HasKey ("highScore")) { //If player doesn't have a high score then set it to zero
			PlayerPrefs.SetInt ("highScore", 0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		highScore = PlayerPrefs.GetInt ("highScore"); //Gets the high score and stores it in an int
		//highScoreText.text = highScore.ToString (); //Turns the high score into text to display
		scoreText.text = "  SCORE: " + currentScore.ToString (); //Turns the current score into text to display
		
		if (currentScore >= highScore) { //If the current score surpasses the high score then ...
			PlayerPrefs.SetInt ("highScore", currentScore); //Update the high score to be the current score
			//highScoreText.text = currentScore.ToString (); //Display the newly updated high score in text
		}
	}
}
