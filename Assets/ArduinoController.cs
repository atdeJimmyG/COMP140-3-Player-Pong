using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

public class ArduinoController : MonoBehaviour
{

	public GameObject playerOne;
	public GameObject playerTwo;
	public bool controllerActive = false;
	public int commPort = 4;

	private bool firstValue;
	private int lastValue;

	private SerialPort serial = null;
	private bool connected = false;

	// Use this for initialization
	void Start()
	{
		ConnectToSerial();
	}

	void ConnectToSerial()
	{
		Debug.Log("Attempting Serial: " + commPort);

		// Read this: https://support.microsoft.com/en-us/help/115831/howto-specify-serial-ports-larger-than-com9
		serial = new SerialPort("\\\\.\\COM" + commPort, 9600);
		serial.ReadTimeout = 50;
		serial.Open();

	}

	// Update is called once per frame
	void Update()
	{
		if (controllerActive)
		{
			WriteToArduino("I");                // Ask for the positions
			String value = ReadFromArduino(50); // read the positions

			if (value != null)                  // check to see if we got what we need
			{
				// EXPECTED VALUE FORMAT: "0-1023"
				string[] values = value.Split('-');     // split the values

				if (values.Length == 2)
				{
					positionPlayers(values);
				}
			}
		}
	}

	void positionPlayers(String[] values)
	{
		if (playerOne != null)
		{
			int newValue = int.Parse(values[0]);
			if (0 < newValue && newValue < 40)
			{
				Debug.Log("Absoulte: " + Mathf.Abs(newValue - lastValue));
				Debug.Log("Input Value: " + newValue);


				if (Mathf.Abs(newValue - lastValue) < 2 || firstValue)
				{
					float newYPosValue = Mathf.Lerp(newValue, lastValue, 0.5f);
					float yPos = Remap(newYPosValue, 0, 30, 0.8f, 3.15f);         // scale the input. this could be done on the Arduino as well.
					float xPos = Remap(newYPosValue, 0, 30, -3.9f, -1.45f);

					Vector3 newPosition = new Vector3(xPos,       // create a new Vector for the position
						yPos, playerOne.transform.position.z);

					playerOne.transform.position = newPosition;        // apply the new position
					if (firstValue)
					{
						firstValue = !firstValue;
					}
				}
				lastValue = int.Parse(values[0]);
			}
		}
	}

	void WriteToArduino(string message)
	{
		serial.WriteLine(message);
		serial.BaseStream.Flush();
	}

	public string ReadFromArduino(int timeout = 0)
	{
		serial.ReadTimeout = timeout;
		try
		{
			return serial.ReadLine();
		}
		catch (TimeoutException e)
		{
			Debug.Log("Timeout");
			return null;
		}
	}

	// be sure to close the serial when the game ends.
	void OnDestroy()
	{
		Debug.Log("Exiting");
		serial.Close();
	}

	// https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
	float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}