using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public Sprite damageSprite;
	public int hp = 4;
	private SpriteRenderer spriteRenderer;
	private GameObject player;
	public bool visibilityCheck = true;
	bool isVisible;
	float blinking;
	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<Renderer>().enabled = false;
		isVisible = false;
		blinking = 2.0f;
	}
	public void Blinkit ()
	{
		if (!isVisible) {
			blinking = 0.0f;
		}
	}
	public void SwapVisibility()
	{
		if (visibilityCheck) {
			GetComponent<Renderer> ().enabled = true;
			visibilityCheck = false;
		} else {
			visibilityCheck = true;
		}
	}

	public void DamageWall(int loss){
		spriteRenderer.sprite = damageSprite;
		hp -= loss;
		if (hp <= 0) {
			gameObject.SetActive (false);
		}
	}

	public static double EaseOut(double t, double b, double c, double d)
	{
		return c*((t=t/d-1)*t*t*t*t + 1) + b;
	}

	void Update () {
		if (isVisible) {
				if (visibilityCheck ) {
					float unit = 10.0f;
					float delta = Vector3.Distance (player.transform.position, transform.position) / unit;
					float distance = 1.0f - delta;
					Color col = GetComponent<Renderer> ().material.color;
					col.a = distance;
					GetComponent<Renderer> ().material.color = col;
				} else {
					GetComponent<Renderer> ().material.color = Color.white;
					GetComponent<Renderer> ().enabled = true;
				}
		} else {
			if (blinking <= 2.0f) {
				blinking += Time.deltaTime;
				float ease = (float)EaseOut (blinking, 0, 1, 3.0f);
				Color col = Color.white;
				GetComponent<Renderer> ().enabled = true;

				if (ease > 1.0f) {
					blinking = 2.0f;
					//GetComponent<Renderer> ().enabled = isVisible;
				}
				col.a = 1 - ease;
				GetComponent<Renderer> ().material.color = col;
			} 
			else {
				GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	void OnMouseDown()
	{

		//gameObject.renderer.enabled = !gameObject.renderer.enabled;
		//	gameObject.SetActive (!gameObject.activ);
		float delta = Vector3.Distance (player.transform.position, transform.position);
		if (delta < 2) {
			GetComponent<Renderer> ().enabled = !GetComponent<Renderer> ().enabled;
			isVisible = true;
		}
	}
}
