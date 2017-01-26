using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(XBoxController))]
public class XBoxControllerTest : MonoBehaviour {

	private XBoxController xboxController;

	[SerializeField]
	private Image arrowLeft;

	[SerializeField]
	private Image arrowRight;

	[SerializeField]
	private Image arrowUp;

	[SerializeField]
	private Image arrowDown;

	// Use this for initialization
	void Awake () {

		xboxController = GetComponent<XBoxController>();
		xboxController.OnDown += OnDown;
		xboxController.OnLeft += OnLeft;
		xboxController.OnRight += OnRight;
		xboxController.OnUp += OnUp;
	}

	private IEnumerator PressArrow(Image arrow)
	{
		Color c = arrow.color;
		c.a = 0.3f;
		arrow.color = c;
		yield return new WaitForSeconds(0.1f);
		c.a = 1f;
		arrow.color = c;
	}

	void OnUp()
	{
		StartCoroutine(PressArrow(arrowUp));
	}

	void OnRight()
	{
		StartCoroutine(PressArrow(arrowRight));
	}

	void OnLeft()
	{
		StartCoroutine(PressArrow(arrowLeft));
	}

	void OnDown()
	{
		StartCoroutine(PressArrow(arrowDown));
	}
}
