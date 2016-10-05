using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	private float firingRate; //Sets the firing rate for the enemyFormation
	public GameObject EnemyPrefabLaser; //Enemies' respective laser that they are given
	public float projectileSpeed; //Speed of enemy laser
	private GameObject EnemyLaser; //Enemies' current instance of their laser
	private int initialEnemies; //The initial number of enemies
	private int currentEnemies; //The current number of enemies
	private float initialFiringRate; //The original firing rate
	
	// Use this for initialization
	void Start ()
	{
		firingRate = 0.0008f;
		projectileSpeed = 5f;
		initialEnemies = SpawnEnemy.totalEnemies; //Sets initial enemies to the number of enemies created
		currentEnemies = initialEnemies; //Sets current enemies to initial enemies
		initialFiringRate = firingRate; //Sets initial firing rate to current firing rate
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (SpawnEnemy.totalEnemies < currentEnemies) { //Every time an enemy dies
			firingRate += ((initialFiringRate * 2) / initialEnemies); //Increase firing rate linearly by dividing the initial firing rate by the initial enemies. ((.0008/32) + current firing rate)
			currentEnemies = SpawnEnemy.totalEnemies; //Sets current number of enemies equal to the total enemies left in enemyFormation to prevent multiple increases of firing rate
		}
		
		float randomNum = Random.value; //Creates random number between 0 and 1
		if (randomNum <= firingRate && enemyController.peace == false) { //If the number is less than the firing rate ... (x < .0008)
			if (EnemyPrefabLaser.name == "Red2") { //If the enemy's laser is the Red2 prefab...
				EnemyLaser = Instantiate (EnemyPrefabLaser, new Vector3 (transform.position.x, transform.position.y - 1), Quaternion.Euler (-45, 45, 0)) as GameObject; //Create two Gameobjects that are set at 45 degrees from down
				EnemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed, -projectileSpeed);
				EnemyLaser = Instantiate (EnemyPrefabLaser, new Vector3 (transform.position.x, transform.position.y - 1), Quaternion.Euler (-45, -45, 0)) as GameObject; //^^^^^^^^
				EnemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-projectileSpeed, -projectileSpeed);
			} else {
				EnemyLaser = Instantiate (EnemyPrefabLaser, new Vector3 (transform.position.x, transform.position.y - 1), Quaternion.identity) as GameObject; // Create the normal enemy prefab laser
				EnemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -projectileSpeed);
			}
			if (PlayerPrefs.GetInt ("allowSFXToPlay") == 1) {
				EnemyLaser.GetComponent<AudioSource> ().Play ();
			}
		}
	}
}