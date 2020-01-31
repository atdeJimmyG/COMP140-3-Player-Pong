using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUpdate : MonoBehaviour
{

    public int id;

    // Update is called once per frame
    void Update()
    {
        switch(id)
		{
			case(1):
                GetComponent<TextMeshProUGUI>().text = GameControlller.player1Score.ToString();
				break;

            case (2):
                GetComponent<TextMeshProUGUI>().text = GameControlller.player2Score.ToString();
                break;

            case (3):
                GetComponent<TextMeshProUGUI>().text = GameControlller.player3Score.ToString();
                break;
        }
    }
}
