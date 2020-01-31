using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

public class ArduinoController : MonoBehaviour
{

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public bool controllerActive = false;
    public int commPort = 4;

    private bool firstValueP1 = true;
    private int lastValueP1;

    private bool firstValueP2 = true;
    private int lastValueP2;

    private bool firstValueP3 = true;
    private int lastValueP3;

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

                if (values.Length == 3)
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
            if (0 < newValue && newValue < 30)
            {
                Debug.Log("Absoulte: " + Mathf.Abs(newValue - lastValueP1));
                Debug.Log("Input Value: " + newValue);


                if (firstValueP1) //Mathf.Abs(newValue - lastValueP1) < 2 || 
                {
                    float newYPosValue = Mathf.Lerp(newValue, lastValueP1, 0.5f);
                    float yPos = Remap(newYPosValue, 0, 30, 0f, 5.25f);         // scale the input. this could be done on the Arduino as well.
                    float xPos = Remap(newYPosValue, 0, 30, -7f, -2f);

                    Vector3 newPosition = new Vector3(xPos,       // create a new Vector for the position
                        yPos, playerOne.transform.position.z);

                    playerOne.transform.position = newPosition;        // apply the new position
                    if (firstValueP1)
                    {
                        firstValueP1 = !firstValueP1;
                    }
                }
                lastValueP1 = int.Parse(values[0]);
            }
        }
        if (playerTwo != null)
        {
            int newValue = int.Parse(values[1]);
            if (0 < newValue && newValue < 30)
            {
                Debug.Log("Absoulte: " + Mathf.Abs(newValue - lastValueP2));
                Debug.Log("Input Value: " + newValue);


                if (firstValueP2) //Mathf.Abs(newValue - lastValueP2) < 2 || 
                {
                    float newYPosValue = Mathf.Lerp(newValue, lastValueP2, 0.5f);
                    float yPos = Remap(newYPosValue, 0, 30, 0.6f, 4.78f);         // scale the input. this could be done on the Arduino as well.
                    float xPos = Remap(newYPosValue, 0, 30, -0.6f, -4.5f);

                    Vector3 newPosition = new Vector3(xPos,       // create a new Vector for the position
                        yPos, playerTwo.transform.position.z);

                    playerTwo.transform.position = newPosition;        // apply the new position
                    if (firstValueP2)
                    {
                        firstValueP2 = !firstValueP2;
                    }
                }
                lastValueP2 = int.Parse(values[1]);
            }
        }
        if (playerThree != null)
        {
            int newValue = int.Parse(values[2]);
            if (0 < newValue && newValue < 30)
            {
                Debug.Log("Absoulte: " + Mathf.Abs(newValue - lastValueP3));
                Debug.Log("Input Value: " + newValue);


                if (firstValueP3) //Mathf.Abs(newValue - lastValueP3) < 2 ||
                {
                    float newYPosValue = Mathf.Lerp(newValue, lastValueP3, 0.5f);
                    float xPos = Remap(newYPosValue, 0, 40, 3f, -3f);        // scale the input. this could be done on the Arduino as well.

                    Vector3 newPosition = new Vector3(xPos,       // create a new Vector for the position
                        playerThree.transform.position.y, playerThree.transform.position.z);

                    playerThree.transform.position = newPosition;        // apply the new position
                    if (firstValueP3)
                    {
                        firstValueP3 = !firstValueP3;
                    }
                }
                lastValueP3 = int.Parse(values[2]);
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
            Debug.LogError(e);
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