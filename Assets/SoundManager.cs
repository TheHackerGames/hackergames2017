using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource sfx;
	public AudioSource sfx3d;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void PlaySound(AudioClip clip){
		sfx.clip = clip;
		sfx.Play();
	}

	public void Play3DSound(AudioClip clip, Vector3 location){
		transform.position = location;
		sfx3d.clip = clip;
		sfx3d.Play();
	}
}
