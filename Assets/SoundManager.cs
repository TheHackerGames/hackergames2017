using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource sfx;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {		
		sfx = GetComponent<AudioSource> ();
	}

	public void PlaySound(AudioClip clip){
		sfx.clip = clip;
		sfx.Play();
	}

}
