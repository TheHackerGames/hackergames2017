using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSonarHit : MonoBehaviour {

	Wall wall;
	void Awake(){
		wall = GetComponent<Wall> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Bullet") {
			wall.Blinkit ();
			//spriteRenderer.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		//spriteRenderer.enabled = true;
	}
}
