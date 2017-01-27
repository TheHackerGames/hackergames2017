using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public static ItemManager instance = null;

	GameObject items;
	int nextItem = 0;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	// Use this for initialization
	void Start () {
		items = GameObject.Find ("Items");
		foreach (Transform t in items.transform) {
			t.gameObject.SetActive(false);
		}
		SpawnNextItem ();
	}

	public void SpawnNextItem(){
		if (nextItem < items.transform.childCount) {
			items.transform.GetChild (nextItem).gameObject.SetActive (true);
			nextItem++;
		}
	}
	

}
