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
		blinking = 1.0f;
	}
	public void Blinkit ()
	{
		blinking = 0.0f;
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

	void Update () {
		if (blinking < 1.0f && isVisible==false) {
			blinking += Time.deltaTime;
			Color col = Color.white;
			GetComponent<Renderer> ().enabled = true;

			if (blinking > 1.0f) {
				blinking = 1.0f;
				GetComponent<Renderer> ().enabled = isVisible;
			}
			col.a = 1.0f - blinking;
			GetComponent<Renderer> ().material.color = col;

		} else {

			//float delta = (player.transform.position - transform.position).distance;
			if (visibilityCheck) {
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
