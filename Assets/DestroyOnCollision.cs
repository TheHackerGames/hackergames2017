using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

	public float maxLifeTime = 0.5f;
	float age = 0.0f;

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}

	void Update() {
		age += Time.deltaTime;
		if (age > maxLifeTime) {
			Destroy (gameObject);
		}
	}
}
