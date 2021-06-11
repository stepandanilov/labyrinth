using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject AI1;
    public GameObject AI2;
    public GameObject player;
    public GameObject AIMO;
    public void StartGame()
    {
        Utils.InitializeVariables();
        if (PlayerPrefs.GetInt("type") <= 2)
        {
            switch (PlayerPrefs.GetInt("diff"))
            {
                case 0:
                    AI1.SetActive(true);
                    AI1.GetComponent<AIManager>().StartBot();
                    break;
                case 1:
                    AI1.SetActive(true);
                    AI1.GetComponent<AIManager>().StartBot();
                    break;
                case 2:
                    AI2.SetActive(true);
                    AI2.GetComponent<AIManager>().StartBot2();
                    break;
                case 3:
                    AI2.SetActive(true);
                    AI2.GetComponent<AIManager>().StartBot2();
                    break;
                case 4:
                    AIMO.SetActive(true);
                    break;
            }
        }
        player.GetComponent<MovementScript>().enabled = true;
        Utils.gameStarted = true;
    }
}
