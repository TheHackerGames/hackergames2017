using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	[SerializeField]
	private Text sonarsText;

	[SerializeField]
	private Text collectiblesRemaining;


	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private GameModel gameModel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		sonarsText.text = string.Format("Sonars: {0}", gameModel.NumSonarChargers);
		collectiblesRemaining.text = string.Format("Items remaining: {0}", gameModel.NumCollectiblesRemaining);
		scoreText.text = string.Format("Score: {0}", gameModel.Score);

	}
}
