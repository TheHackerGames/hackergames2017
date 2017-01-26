using System;
using UnityEngine;


public class XBoxController: MonoBehaviour
{
	private const string padLeft = "d pad left";
	private const string padRight = "d pad right";
	private const string padUp = "d pad up";
	private const string padDown = "d pad down";

	public Action OnLeft;
	public Action OnRight;
	public Action OnUp;
	public Action OnDown;

	public void Update()
	{
		if (Input.GetButtonDown(padLeft) && OnLeft != null)
		{
			OnLeft();
		}

		if (Input.GetButtonDown(padRight) && OnRight != null)
		{
			OnRight();
		}

		if (Input.GetButtonDown(padUp) && OnUp != null)
		{
			OnUp();
		}

		if (Input.GetButtonDown(padDown) && OnDown != null)
		{
			OnDown();
		}
	}
}


