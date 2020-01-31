using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    public float speed = 5f;
    public int lastPlayerToHit;


    // Start is called before the first frame update
    void Start()
    {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;

        GetComponent<Rigidbody>().velocity = new Vector3(speed * sx, speed * sy, 0f);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player1")
        {
            Debug.Log("Player1");
            lastPlayerToHit = 1;
        }
        else if (collision.gameObject.name == "Player2")
        {
            Debug.Log("Player2");
            lastPlayerToHit = 2;
        }
        else if (collision.gameObject.name == "Player3")
        {
            Debug.Log("Player3");
            lastPlayerToHit = 3;
        }

        if (collision.gameObject.name == "Wall")
        {
            Score();
        }
    }

    void Score()
    {
        if (lastPlayerToHit == 1)
        {
            GameControlller.player1Score += 1;
            Debug.Log(GameControlller.player1Score);
        }
        else if(lastPlayerToHit == 2)
        {
            GameControlller.player2Score += 1;
            Debug.Log(GameControlller.player2Score);
        }
        else
        {
            GameControlller.player3Score += 1;
            Debug.Log(GameControlller.player3Score);
        }
        Destroy(gameObject);
    }
}
