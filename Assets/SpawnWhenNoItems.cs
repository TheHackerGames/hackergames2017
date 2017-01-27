using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhenNoItems : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	AudioSource audioSource;
	BoxCollider2D boxCollider;
	bool spawned = false;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
		boxCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawned) {
			var itemsLeft = GameObject.FindGameObjectsWithTag ("Food");
			if (itemsLeft.Length == 0) {
				Activate ();
				spawned = true;
				Destroy (this);
			}
		}
	}

	void Activate(){
		spriteRenderer.enabled = true;
		audioSource.enabled = true;
		boxCollider.enabled = true;
	}
}
