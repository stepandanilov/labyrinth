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
            switch (PlayerPrefs.GetInt("diff"))
            {
                case 0:
                    AI1.SetActive(true);
                    break;
                case 1:
                    AI1.SetActive(true);
                    break;
                case 2:
                    AI2.SetActive(true);
                    break;
                case 3:
                    AI2.SetActive(true);
                    break;
            }
            if (AI1.activeSelf) AI1.GetComponent<AIManager>().StartBot();
            if (AI2.activeSelf) AI2.GetComponent<AIManager>().StartBot2();
        }
        

        player.GetComponent<MovementScript>().enabled = true;
    }
}
