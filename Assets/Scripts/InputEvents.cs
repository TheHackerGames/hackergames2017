using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown()
	{
		//gameObject.renderer.enabled = !gameObject.renderer.enabled;
		//	gameObject.SetActive (!gameObject.activ);
		GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
	}
}
