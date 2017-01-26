using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAudio : MonoBehaviour {

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
