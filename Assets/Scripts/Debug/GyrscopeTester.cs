using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GyrscopeTester : MonoBehaviour {

	[SerializeField]
	private ArduinoGyroscope gyroscope;

	SerialCommunicator serial;

	[SerializeField]
	private Text debugText;

	private Image image;
	// Use this for initialization
	void Awake () 
	{
		image = GetComponent<Image>();
		serial = gyroscope.GetComponent<SerialCommunicator>();
		serial.OnArduinoConnected += OnArduinoConnected;
		serial.OnArduinoConnectionFailed += OnArduinoConnectionFailed;
	}

	private void OnArduinoConnected()
	{
		debugText.text = "Arduino connected, calibrating";
	}

	private void OnArduinoConnectionFailed()
	{
		debugText.text = "No arduino detected";
	}

	private void OnDestroy()
	{
		serial.OnArduinoConnected -= OnArduinoConnected;
		serial.OnArduinoConnectionFailed -= OnArduinoConnectionFailed;
	}
	
	// Update is called once per frame
	void Update () {

		Color c = image.color;
		c.a = gyroscope.IsCalibrated ? 1f : 0.3f;
		image.color = c;

		if(gyroscope.IsCalibrated)
		{
			debugText.text = "Calibration finished: " + gyroscope.GetGyroData().X;
			image.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, -gyroscope.GetGyroData().X));
		}
	}
}
