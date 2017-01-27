using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRTF : MonoBehaviour {

	AudioLowPassFilter filter;
	GameObject player;
	public float angle;
	public float minCutOff = 200;
	public float maxCutOff = 400;
	public float minReverb = 1;
	public float maxReverb = 10;
	// Use this for initialization
	void Awake () {
		filter = GetComponent<AudioLowPassFilter> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	float SignedAngle(Vector2 a, Vector2 b){
		var angle = Vector2.Angle(a, b);
		var cross = Vector3.Cross(a, b);
		if (cross.y < 0) angle = -angle;
		return angle;
	}

	// Update is called once per frame
	void Update () {
		
		Vector2 forward = player.transform.right;
		Vector2 direction = (transform.position - player.transform.position).normalized;

		angle = SignedAngle (forward, direction);

		filter.cutoffFrequency = Mathf.Lerp(maxCutOff, minCutOff, angle/180.0f);

	}
}
