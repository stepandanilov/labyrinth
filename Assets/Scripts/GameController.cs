using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject AI1;
    public GameObject AI2;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        Utils.InitializeVariables();
        if (PlayerPrefs.GetInt("type") <= 2)
        {
            AI1.GetComponent<AIManager>().StartBot();
            AI2.GetComponent<AIManager>().StartBot2();
        }
        player.GetComponent<MovementScript>().enabled = true;
    }
}
