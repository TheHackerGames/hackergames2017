using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public Sprite damageSprite;
	public int hp = 4;
	private SpriteRenderer spriteRenderer;
	private GameObject player;
	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void DamageWall(int loss){
		spriteRenderer.sprite = damageSprite;
		hp -= loss;
		if (hp <= 0) {
			gameObject.SetActive (false);
		}
	}

	void Update () {
		//float delta = (player.transform.position - transform.position).distance;
		float unit = 5.0f;
		float delta = Vector3.Distance(player.transform.position, transform.position)/unit;
		float distance = 1.0f - delta;
		Color col = GetComponent<Renderer> ().material.color;
		col.a = distance;
		GetComponent<Renderer>().material.color = col;
	}
}
