using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance = null;

    public List<int> path = new List<int>();
    private GameManager() { }
    public static GameManager getInstance()
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
                    MainUI.getInstance().Victory();
                    break;
                case "AI1":
                    MainUI.getInstance().Lose();
                    break;
                case "AI2":
                    MainUI.getInstance().Lose();
                    break;
            }
        }
    }
}
