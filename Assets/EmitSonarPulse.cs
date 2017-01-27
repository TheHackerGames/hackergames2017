using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitSonarPulse : MonoBehaviour {

	public GameObject sonarBullet;
	public int numBullets = 10;
	public float speed = 1.0f;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 spawnPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float deltaAngle = Mathf.Deg2Rad * (360.0f/numBullets);
			float angle = 0.0f;
			for (int i = 0; i < numBullets; ++i) {
				
				Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				SpawnBullet (spawnPosition, direction);

				angle += deltaAngle;
			}
		}
	}

	void SpawnBullet(Vector2 location, Vector2 direction){
		var velocity = direction.normalized * speed;
		location.x = ((int)(location.x)/13)*13.0f +0.5f;
		location.y = ((int)(location.y)/13)*13.0f +0.5f;
		location += direction.normalized * 0.5f; 
		var bullet = GameObject.Instantiate (sonarBullet, location, Quaternion.identity);

		Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = true;
		rb2d.velocity = velocity;
	}
}
