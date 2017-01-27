using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

	public float maxLifeTime = 0.5f;
	float age = 0.0f;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Wall") {
			Destroy (gameObject);

		}
	}

	void Update() {
		age += Time.deltaTime;
		Color color = spriteRenderer.color;
		color.a = 1f - age / maxLifeTime;
		spriteRenderer.color = color;
		if (age > maxLifeTime) {
			Destroy (gameObject);
		}
	}
}
