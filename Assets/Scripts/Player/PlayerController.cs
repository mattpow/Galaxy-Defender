using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private float velocityX; //Velocity of player's movement before multiplying by deltaTime
	private Vector3 movementRangeMin; //Minimum range object can go due to edge of screen and width of object
	private Vector3 movementRangeMax; // ^^^^^^^^
	private Vector3 bottomLeftWorldCoordinates; //Maps the area of the screen
	private Vector3 topRightWorldCoordinates; //  ^^^^^
	private Vector2 PlayerPos; //Tracks Player's Location
	private bool leftButtonHeld; //UI Button clicked booleans
	private bool rightButtonHeld;
	private bool peacePeriod;
	private bool gameHasStarted;
	private SpriteRenderer playerRend;
	
	public Sprite playerHit; //Sprite that renders when player is hit
	public Sprite playerNormal; //Sprite that renders by default
	
	
	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start ()
	{
		bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint (Vector3.zero); // Determing bottom left of screen
		topRightWorldCoordinates = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0)); // Determining top right of screen for view position purposes
		movementRangeMin = bottomLeftWorldCoordinates + GetComponent<Renderer> ().bounds.extents; // Creating min and max range based off of size of player and screen boundaries
		movementRangeMax = topRightWorldCoordinates - GetComponent<Renderer> ().bounds.extents; // ^^^^^^^^^
		PlayerPos = new Vector2 ((topRightWorldCoordinates.x / 2), transform.position.y); //Starting position of Player
		this.transform.position = PlayerPos; // ^^^^^^^^
		
		playerRend = GetComponent<SpriteRenderer> ();
		rightButtonHeld = false;
		peacePeriod = true;
		gameHasStarted = false;
		leftButtonHeld = false;
		velocityX = 15f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameHasStarted) {
			peacePeriod = true;
			gameHasStarted = true;
			StartCoroutine (playerPeaceWait ());
		} 
		
		if (Input.GetKey (KeyCode.LeftArrow) || leftButtonHeld) { //As long as the button is being held, move in respective direction.
			MoveLeft ();
		}
		if (Input.GetKey (KeyCode.RightArrow) || rightButtonHeld) { //As long as the button is being held, move in respective direction.
			MoveRight ();
		}
	}
	
	
	void MoveLeft ()
	{
		PlayerPos -= new Vector2 (velocityX * Time.smoothDeltaTime, 0f); //Changes player's position at speed of velocity submitted multiplied by deltaTime to adjust for lag		
		PlayerPos.x = Mathf.Clamp (PlayerPos.x, movementRangeMin.x, movementRangeMax.x);	//Clamps player position to be within screen view
		this.transform.position = PlayerPos; //updates the game to go to current position
	}
	
	void MoveRight () //Inverse of MoveLeft()  ^^^^^^^^^^^^^^
	{
		PlayerPos += new Vector2 (velocityX * Time.smoothDeltaTime, 0f);
		PlayerPos.x = Mathf.Clamp (PlayerPos.x, movementRangeMin.x, movementRangeMax.x);	
		this.transform.position = PlayerPos;
	}
	
	public void buttonHeld (string buttonName) //Detects if canvas UI Button is clicked. Checks which button was pressed and marks its click as true
	{
		if (buttonName == "leftButton") {
			leftButtonHeld = true;
		} else if (buttonName == "rightButton") {
			rightButtonHeld = true;
		}
	}
	public void buttonUnHeld (string buttonName) //Detects when canvas UI Button is no longer being clicked. Checks which button to turn boolean Held to false
	{
		if (buttonName == "leftButton") {
			leftButtonHeld = false;
		} else if (buttonName == "rightButton") {
			rightButtonHeld = false;
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (!peacePeriod) {
			EnemyLaser enemyLaser = col.gameObject.GetComponent<EnemyLaser> (); //Create a variable of enemyLaser when hit by one
			if (enemyLaser) {
				StartCoroutine (playerHitAnimation ()); //Starts hit animation
				Destroy (col.gameObject); //Destroys laser
				LifeController.livesLeft--; //Reduces current lives after being hit
				StartCoroutine (playerPeaceWait ());
				PlayerPos = new Vector2 ((topRightWorldCoordinates.x / 2), transform.position.y); //Starting position of Player
				this.transform.position = PlayerPos; // ^^^^^^^^
			}
		}
	}
	
	IEnumerator playerHitAnimation ()
	{
		this.GetComponent<SpriteRenderer> ().sprite = playerHit; //Renders hit sprite 
		yield return new WaitForSeconds (.1f); //Wait for 1/10 of second
		this.GetComponent<SpriteRenderer> ().sprite = playerNormal; //Renders normal sprite again
		StopCoroutine (playerHitAnimation ()); //Stops hit animation
	}
	
	IEnumerator playerPeaceWait ()
	{
		peacePeriod = true;
		float f = 0.0f;
		playerRend.enabled = true;
		playerRend.material.color = Color.clear;
		
		while (f < 1.0f) {
			yield return null;
			f += Time.deltaTime;
			playerRend.material.color = new Color (1, 1, 1, Mathf.Lerp (0, 1, f * f * (3.0f - 2.0f * f)));
		}
		playerRend.material.color = Color.white;
		yield return new WaitForSeconds (2f);
		peacePeriod = false;
		StopCoroutine (playerPeaceWait ());
	}
}
