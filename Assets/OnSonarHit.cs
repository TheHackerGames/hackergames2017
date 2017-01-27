using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSonarHit : MonoBehaviour {

	Renderer spriteRenderer;
	void Awake(){
		spriteRenderer = GetComponent<Renderer> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Bullet") {
			spriteRenderer.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		spriteRenderer.enabled = true;
	}
}
