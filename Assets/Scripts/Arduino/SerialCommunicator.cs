using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialCommunicator : MonoBehaviour 
{
	private SerialPort serialPort;

	private bool communicationInitialised = false;

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
				string chars = "";
				for(int i =0; i < 14; i++)
				{
					chars += serialPort.ReadChar();
					
				}
		
				Debug.Log(chars);
			}
			catch(Exception e)
			{
				serialPort.Write("ping");
			}
		}
	}
}
