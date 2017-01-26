using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudio : MonoBehaviour {

	public float distance = 4;
	AudioSource audioSource;
	GameObject player;
	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		audioSource.enabled = (transform.position - player.transform.position).magnitude < distance;
	}
}
