using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameModel : MonoBehaviour {

	public int NumSonarChargers = 3;
	public int Score;

	private List<Collectible> collectibles = new List<Collectible>();

	void Start () {

		collectibles = GameObject.FindObjectsOfType<Collectible>().ToList();
		Debug.Log(GameObject.FindObjectsOfType<Collectible>().Length);
	}

	public void Collect(Collectible collectible)
	{
		collectibles.Remove(collectible);
		Score += collectible.Score;
	}

	public int NumCollectiblesRemaining
	{
		get 
		{
			return collectibles.Count;
		}
	}
}
