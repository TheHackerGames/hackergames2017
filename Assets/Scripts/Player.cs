using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject {

	public enum MovementType
	{
		Relative,
		Absolute
	}
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 10;
	public float restartLevelDelay = 1;
	public Text foodText;
	public MovementType movementType = MovementType.Absolute;
	private Animator animator;
	private int food;

	// Use this for initialization
	protected override void Start () {		
		animator = GetComponent<Animator> ();
		food = GameManager.instance.playerFoodPoints;
		foodText.text = "Food " + food;
		base.Start ();
	}

	void OnDisable() {
		GameManager.instance.playerFoodPoints = food;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.playersTurn)
			return;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");


		if (movementType == MovementType.Relative) {
			Vector2 dir = transform.right;	
			int xdir = Mathf.RoundToInt (dir.x);
			int ydir = Mathf.RoundToInt (dir.y);
			if (vertical == 1 || vertical == -1) {			
				AttemptMove<Wall> (xdir, ydir);
			}

			if (horizontal == 1 || horizontal == -1) {
				Rotate (horizontal);
			}

		} else if (movementType == MovementType.Absolute) {
			if (horizontal != 0)
				vertical = 0;

			if (horizontal != 0 || vertical != 0)
				AttemptMove<Wall> (horizontal, vertical);
		}
	}

	protected override void Rotate(int horizontal){
		if (movementType == MovementType.Relative) {
			food--;
			foodText.text = "Rotate " + food;
			base.Rotate (horizontal);
			CheckIfGameOver ();
			GameManager.instance.playersTurn = false;
		}
	}

	protected override void AttemptMove<T>(int xdir, int ydir){
		
		food--;
		foodText.text = "Food " + food;
		base.AttemptMove<T> (xdir, ydir);

		//RaycastHit2D raycast;
		CheckIfGameOver ();
		GameManager.instance.playersTurn = false;
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		var pos = gameObject.transform.position;
		var pos2 = other.transform.position;

		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " Food " + food;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Soda") {
			food += pointsPerSoda;
			foodText.text = "+" + pointsPerFood + " Food " + food;
			other.gameObject.SetActive (false);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("PlayerChop");
	}

	private void Restart()
	{
		SceneManager.LoadScene (0);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger ("PlayerHit");
		food -= loss;
		foodText.text = "-" + loss + " Food " + food;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver() 
	{
		if (food <= 0) {
			GameManager.instance.GameOver ();
		}
	}
}
