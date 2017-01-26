using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GyroData
{
	public readonly int X;
	public readonly int Y;
	public readonly int Z;

	public GyroData(int x, int y, int z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}
}

[RequireComponent(typeof(SerialCommunicator))]
public class ArduinoGyroscope : MonoBehaviour {

	[SerializeField]
	private int calibrationTime = 30;

	private SerialCommunicator serial;

	private int x;
	private int y;
	private int z;

	private int pointZeroX;
	private int pointZeroY;
	private int pointZeroZ;

	public bool IsCalibrated { get; private set; }
	private bool isCalibrating = false;

	void Awake () 
	{
		serial = GetComponent<SerialCommunicator>();
		serial.OnGyroscopeData += OnGyroscopeData;
	}

	private void OnGyroscopeData(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;

		if(!IsCalibrated && !isCalibrating)
		{
			StartCoroutine(Calibrate());
		}
	}

	private IEnumerator Calibrate()
	{
		int secondsRemaining = calibrationTime;

		isCalibrating = true;
		Debug.Log("Calibration started");

		while(secondsRemaining > 0)
		{
			yield return new WaitForSeconds(1);
			secondsRemaining--;
			Debug.LogFormat("Calibrating ({0})", secondsRemaining);
		}
		pointZeroX = x;
		pointZeroY = y;
		pointZeroZ = z;
		Debug.Log("Calibration finished");
		IsCalibrated = true;

	}

	
	public GyroData GetGyroData()
	{
		if(!IsCalibrated)
		{
			throw new UnityException("Gyroscope is not calibrated yet");
		}

		return new GyroData(x - pointZeroX, y - pointZeroY, z - pointZeroZ);
	}
}
