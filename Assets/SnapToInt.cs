using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToInt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int x = Mathf.RoundToInt (transform.position.x);
		int y = Mathf.RoundToInt (transform.position.y);
		Vector3 v = new Vector3 (x, y, transform.position.z);
		transform.position = v;
		Destroy (this);
	}
	

}
