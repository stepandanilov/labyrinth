using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance = null;
    private GameManager() { }
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }
    public void CollisionCheck(Collision2D collision, Transform obj)
    {
        if (collision.transform.name.StartsWith("Finish"))
        {
            switch (obj.name)
            {
                case "Player":
                    MainUI.GetInstance().Victory();
                    break;
                case "AI1":
                    MainUI.GetInstance().Lose();
                    break;
                case "AI2":
                    MainUI.GetInstance().Lose();
                    break;
                case "AI_MO":
                    MainUI.GetInstance().Lose();
                    break;
            }
            Utils.gameStarted = false;
        }
    }
}
