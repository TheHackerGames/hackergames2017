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
	private int food;
	private float blink = 1.0f;
	private GameObject theSprite;
	// Use this for initialization
	public AudioClip hitWallSound;
	public AudioClip hitOutterWallSound;
	public AudioClip moveForward;
	public AudioClip moveBackward;
	public AudioClip turnLeft;
	public AudioClip turnRight;

	protected override void Start () {		

		food = GameManager.instance.playerFoodPoints;
		foodText.text = "Score " + food;
		base.Start ();
		theSprite = this.gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject;
	}

	void OnDisable() {
		GameManager.instance.playerFoodPoints = food;
	}
	void UpdateBlink()
	{
		
		theSprite.SetActive (blink<1.0f);
		if (blink > 1.0f)
			blink = 1.0f;
		//Color col = theSprite.GetComponent<Renderer> ().material.color;
		//col.a = blink;

		//return;
		//Color col = GetComponent<Renderer> ().material.color;
		//col.a = blink;
	//	GetComponentWithChildren<Renderer>().material.color = col;

		/*Component[] renderers = GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer curRenderer in renderers)
		{
			Color color;
			foreach (Material material in curRenderer.materials)
			{
				color = material.color;
				// change alfa for transparency
				color.a = blink;
				material.color = color;
			}
		}*/
	}

	void BlinkIt()
	{
		blink = 0.5f;
		UpdateBlink ();
	}

	// Update is called once per frame
	protected override void Update () {

		base.Update();

		if (!GameManager.instance.playersTurn)
			return;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");


		if (movementType == MovementType.Relative) {

			//Vector2 dir = transform.right * vertical;	
			//Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			Vector2 dir = (Vector2)(Quaternion.Euler(0,0,baseRotation) * Vector2.right);
			int xdir = Mathf.RoundToInt (dir.x);
			int ydir = Mathf.RoundToInt (dir.y);
			if (vertical == 1 || vertical == -1) {			
				if (!moving) {
					if (vertical > 0) {
						SoundManager.instance.PlaySound (moveForward);
					} else {
						SoundManager.instance.PlaySound (moveBackward);
					}
				}
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
		if (blink < 1.0f) {
			blink += 1.0f * Time.deltaTime;
			UpdateBlink ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameManager.instance.ShowWalls ();
		}

	}

	protected override void Rotate(int horizontal){
		if (movementType == MovementType.Relative) {

			if (!rotating) {
				if (horizontal > 0) {
					SoundManager.instance.PlaySound (turnLeft);
				} else {
					SoundManager.instance.PlaySound (turnRight);
				}
			}

			food--;
			foodText.text = "Rotate " + food;
			base.Rotate (horizontal);
			CheckIfGameOver ();
			GameManager.instance.playersTurn = false;
		}
	}

	protected override void AttemptMove<T>(int xdir, int ydir){


		food--;

		foodText.text = "Score " + food;
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
			foodText.text = "+" + pointsPerFood + " Score " + food;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Soda") {
			food += pointsPerSoda;
			foodText.text = "+" + pointsPerFood + " Score " + food;
			other.gameObject.SetActive (false);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		if (component.tag == "Wall") {
			SoundManager.instance.PlaySound (hitWallSound);
		} else {
			SoundManager.instance.PlaySound (hitOutterWallSound);
		}
		BlinkIt ();
		/*Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("PlayerChop");*/
	}

	private void Restart()
	{
		SceneManager.LoadScene (0);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger ("PlayerHit");
		food -= loss;
		foodText.text = "-" + loss + " Score " + food;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver() 
	{
		if (food <= 0) {
			food = 0;
			//GameManager.instance.GameOver ();
		}
	}
}
