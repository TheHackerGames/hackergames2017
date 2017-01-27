using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;

public class SerialCommunicator : MonoBehaviour 
{
	public Action<int, int, int> OnGyroscopeData;
	public Action OnArduinoConnected;
	public Action OnArduinoConnectionFailed;
	private SerialPort serialPort;

	private bool communicationInitialised = false;

	[SerializeField]
	private string portAddress = "/dev/tty.usbmodem1411";
	///dev/cu.wchusbserial1420

	[SerializeField]
	private int baudRate = 115200;


	// Use this for initialization
	void Start () {

		serialPort = new SerialPort(portAddress, baudRate);
		serialPort.ReadTimeout = 50;
		try 
		{
			serialPort.Open();
			serialPort.DiscardInBuffer ();
		}
		catch(Exception e)
		{
			Debug.LogWarning("arduino not detected:" + e.ToString());

			if(OnArduinoConnectionFailed != null)
			{
				OnArduinoConnectionFailed();
			}
		}
		finally 
		{
			Debug.Log("port opened");
			if(OnArduinoConnected != null)
			{
				OnArduinoConnected();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(serialPort.IsOpen)
		{
			
			if(serialPort.BytesToRead == 0)
			{
				return;
			}

			if(communicationInitialised)
			{
				while(serialPort.BytesToRead >= 12)
				{
					try 
					{
						
						float x = readFloat();
						float y = readFloat();
						float z = readFloat();

						int eulerX = Convert.ToInt32(x * 180 / Mathf.PI);
						int eulerY =  Convert.ToInt32(y * 180 / Mathf.PI);
						int eulerZ =  Convert.ToInt32(z * 180 / Mathf.PI);

						if(OnGyroscopeData != null)
						{
							OnGyroscopeData(eulerX, eulerY, eulerZ);
						}

					} 
					catch(Exception )
					{
					}
				}

			
			}
			else 
			{
				if(IsCommandThere("Ready"))
				{
					Debug.Log("arduino is ready");
					serialPort.WriteLine("ping");
					communicationInitialised = true;
				}
			}
		}
	}

	float readFloat()
	{
		byte b1 = Convert.ToByte(serialPort.ReadByte());
		byte b2 = Convert.ToByte(serialPort.ReadByte());
		byte b3 = Convert.ToByte(serialPort.ReadByte());
		byte b4 = Convert.ToByte(serialPort.ReadByte());

		byte[] newArray = new[] { b4, b3, b2, b1};

		return BitConverter.ToSingle(newArray, 0);
	}

	private string[] GetDataFromArduino()
	{
		try 
		{
			string data = serialPort.ReadExisting();
			string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			return lines;
		}
		catch(Exception )
		{
		}

		return null;

	}

	private bool IsCommandThere(string command)
	{
		string[] lines = GetDataFromArduino();

		if(lines != null)
		{
			foreach(string line in lines)
			{
				Debug.Log(line);

				if(line.Contains(command))
				{
					return true;
				}

			}
		}

		return false;
	}

	private void OnApplicationQuit ()
	{
		if(serialPort.IsOpen)
		{
			serialPort.Close();
		}

	}
}
