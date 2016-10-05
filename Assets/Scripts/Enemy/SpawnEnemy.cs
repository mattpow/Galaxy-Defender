using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
	public Object[] enemies = {}; //Creates an array of enemy objects
	private int randomNum; //Holds a random number
	public static int totalEnemies; //Holds the total number of enemies in the formation
	// private GameObject enemyPrefabs[]

	// Use this for initialization
	void Start ()
	{	
		Reset ();
	} 
	
	// Update is called once per frame
	void Update ()
	{
		if (totalEnemies == 0) { //When total enemies are equal to 0, start the level over
			Reset ();
		}
	}
	
	void Reset ()
	{
		enemies = Resources.LoadAll ("enemies", typeof(GameObject)); //Finds all variants of enemy and stores them in enemies[] as a gameobject
		enemyController.peace = true;
		totalEnemies = 0;
		
		foreach (Transform child in transform) { //For every enemyPosition object inside of enemyFormation object, Create an enemy inside of it using enemyPrefab. Places enemyPrefab inside (enemyFormation > enemyPosition > enemyPrefab)
			randomNum = Random.Range (0, enemies.Length); //Creates a random int between 0 and size of enemy array
			GameObject enemy = Instantiate (enemies [randomNum], child.transform.position, Quaternion.identity) as GameObject; //Creates the chosen enemy object from random array
			enemy.transform.parent = child; //Makes enemyPrefabs' parent the child of enemyFormation: enemyPosition
			totalEnemies++; //Increases the total enemies after every spawn
		}
		
		Destroy (GameObject.Find ("Chartboost"));
		//Destroy (GameObject.Find ("StoreEvents"));
		//Destroy (GameObject.Find ("CoreEvents"));
		LoseScreen.hasDisplayed = false;
	}	
}