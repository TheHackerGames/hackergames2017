using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialCommunicator : MonoBehaviour 
{
	private SerialPort serialPort;

	// Use this for initialization
	void Start () {

		serialPort = new SerialPort("/dev/tty.usbmodem1411", 115200);
		serialPort.ReadTimeout = 50;
		serialPort.Open();
	}
	
	// Update is called once per frame
	void Update () {

		if(serialPort.IsOpen)
		{
			try 
			{
				
			Debug.Log(serialPort.ReadExisting());
			}
			catch(Exception e)
			{
			}
		}
	}
}
