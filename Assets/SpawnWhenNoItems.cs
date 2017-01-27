using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhenNoItems : MonoBehaviour {

	public AudioClip exitShowSound;
	SpriteRenderer spriteRenderer;
	AudioSource audioSource;
	BoxCollider2D boxCollider;
	bool spawned = false;
	private GameModel gameModel;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		gameModel = GameObject.FindObjectOfType<GameModel>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawned) {
			
			if (gameModel.NumCollectiblesRemaining == 0) {
				Activate ();
				spawned = true;
			
				Destroy (this);
			}
		}
	}

	void Activate(){
		spriteRenderer.enabled = true;
		audioSource.enabled = true;
		boxCollider.enabled = true;
		SoundManager.instance.PlaySound(exitShowSound);
	}
}
