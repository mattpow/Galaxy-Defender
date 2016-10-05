using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour
{

	public GameObject[] lives = { };
	public static int livesLeft = 3;
	
	// Use this for initialization
	void Start ()
	{
		livesLeft = 3;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (livesLeft < lives.Length) {
			Destroy (lives [livesLeft]);
			if (livesLeft <= 0) {
				Application.LoadLevel ("Lose");
			}		
		}
	}
}
