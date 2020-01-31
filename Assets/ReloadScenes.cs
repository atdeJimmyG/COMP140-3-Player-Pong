using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ReloadScenes : MonoBehaviour
{

    void Update()
    {
        if (GameObject.Find("Ball") == null)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
