using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyController : MonoBehaviour
{
	public int maxHits; //Maximum number of hits that enemy can take before being killed
	public Sprite hit; //Hit sprite
	public Sprite normal; //default sprite
	public int enemyPoints; //Points awarded for each enemy variant kill
	
	private SpriteRenderer rend;
	private int currentHits; //Current hits that enemy has received
	private Vector3 bottomLeftWorldCoordinates; // Vectors to determine the square area of the game screen
	private Vector3 topRightWorldCoordinates; // ^^^^
	private Vector3 movementRangeMin; //furthest distance to the LEFT that obejct can go by calculating the position of left edge and width of GameObject
	private Vector3 movementRangeMax; //furthest distance to the RIGHT that obejct can go by calculating the position of left edge and width of GameObject
	private bool goingRight = true; //Keeps track of GameObjects' direction to keep things simple
	private Vector3 deltaPos = new Vector3 (5 * Time.fixedDeltaTime, 0, 0); //Formula for the change in position after every update
	private int initialEnemies;
	public static bool peace = true;
	private bool gameHasStarted = false;
		
	// Use this for initialization
	void Start ()
	{
		bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint (Vector3.zero); // Determing bottom left of screen
		topRightWorldCoordinates = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0)); // Determining top right of screen for view position purposes
		movementRangeMin = bottomLeftWorldCoordinates + GetComponent<Renderer> ().bounds.extents; //Determines width of item and adds that to the edge to create a better boundary
		movementRangeMax = topRightWorldCoordinates - GetComponent<Renderer> ().bounds.extents; // ^^^^^^^^
		
		initialEnemies = SpawnEnemy.totalEnemies;
		currentHits = 0;
		rend = GetComponent<SpriteRenderer> ();
			
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (initialEnemies == SpawnEnemy.totalEnemies && gameHasStarted == false) {
			peace = true;
			gameHasStarted = true;
			StartCoroutine (peaceWait ());
		} else if (SpawnEnemy.totalEnemies == 0) {
			gameHasStarted = false;
		}
	
		
		if (goingRight && !peace) { //Going right by default. If going right then update position to the right
			if (transform.position.x <= movementRangeMax.x) {
				this.transform.position += deltaPos; //update enemyPos to new location
			} else { 
				goingRight = false; // If all the way to the right, start moving left
			}
		}
		
		if (!goingRight && !peace) { //Going left until GameObject reaches left edge
			if (transform.position.x >= movementRangeMin.x) {
				this.transform.position -= deltaPos; //Subtract enemyPos to new location
			} else {
				goingRight = true; //Once all the way to the left, update to start going right again
			}
		}
		
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		PlayerLaser laser = col.gameObject.GetComponent<PlayerLaser> (); // REFER TO PLAYER ATTACK COLLISION
		if (laser && peace == false) {
			StartCoroutine (hitAnimation ());
			Destroy (col.gameObject);
			currentHits++; //increments hit count
			if (currentHits >= maxHits) { //if hit count is equal to maximum hits then ...
				Score.currentScore += enemyPoints; //Increase game score by the enemy's point allocation
				Destroy (gameObject); //Destroy enemy
				SpawnEnemy.totalEnemies--; //Decrease total number of enemies in formation
			}
		}
	}	
	
	IEnumerator hitAnimation ()
	{
		this.GetComponent<SpriteRenderer> ().sprite = hit;
		yield return new WaitForSeconds (.1f);
		this.GetComponent<SpriteRenderer> ().sprite = normal;
		StopCoroutine (hitAnimation ());
	}
	
	IEnumerator peaceWait ()
	{
	
		float f = 0.0f;
		rend.enabled = true;
		rend.material.color = Color.clear;
		
		while (f < 1.0f) {
			yield return null;
			f += Time.deltaTime;
			rend.material.color = new Color (1, 1, 1, Mathf.Lerp (0, 1, f * f * (3.0f - 2.0f * f)));
		}
		rend.material.color = Color.white;
		peace = false;
		StopCoroutine (peaceWait ());
	}
}
