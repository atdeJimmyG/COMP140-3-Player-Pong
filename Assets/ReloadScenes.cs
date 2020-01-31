using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ReloadScenes : MonoBehaviour
{

    public GameObject ball;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ball") == null)
        {
            Instantiate(ball, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
