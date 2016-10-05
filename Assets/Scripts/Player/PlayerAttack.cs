using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	private bool coroutineInProgress;
	
	public GameObject currentPlayerLaser; //Current laser being used by player
	private float projectileSpeed; //velocity of emitted lasers
	
	// Use this for initialization
	void Start ()
	{
		projectileSpeed = 6f;
		coroutineInProgress = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!coroutineInProgress) { //Starts coroutine and only allows one at a time to prevent infinite lasers at once
			StartCoroutine (WaitAndShoot ()); 
		}
	}
	
	IEnumerator WaitAndShoot ()
	{
		coroutineInProgress = true; //sets coroutine to true to prevent duplicates
		if (!enemyController.peace) {
			yield return new WaitForSeconds (0.5f); //Wait time between laser emissions (firing rate)
			GameObject playerLaser = Instantiate (currentPlayerLaser, new Vector3 (transform.position.x, transform.position.y + .8f), Quaternion.identity) as GameObject; //Creates laser
			playerLaser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, projectileSpeed); //Sets velocity of newly created laser
			if (PlayerPrefs.GetInt ("allowSFXToPlay") == 1) {
				playerLaser.GetComponent<AudioSource> ().Play ();
			}
		}
		coroutineInProgress = false; //Sets coroutine in progress to false to allow another one to be created
		StopCoroutine (WaitAndShoot ()); //Stops this instance of coroutine
	}
}
