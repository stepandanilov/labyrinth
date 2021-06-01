using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCondition : MonoBehaviour
{
    //private Vector2 offset = new Vector2(0.5f, (1f/Mathf.Sqrt(12)));

    //private int indexX = 1;
    //private int indexY = 2;
    //private float x;
    //private float y;
    //private float triangleSide = 1.5f;

    //private void Start()
    //{
    //    offset *= triangleSide;
    //    if (indexY % 2 == 0)
    //    {  
    //        x = (indexX + indexY / 4f)* triangleSide;
    //        y = (indexY * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f;
    //        transform.position = new Vector2(x, y) + offset;
    //    }
    //    else
    //    {
    //        x = (indexX + 1.25f + indexY / 4f) * triangleSide; 
    //        y = ((indexY + 1) * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f;
    //        transform.position = new Vector2(x, y) - offset;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.StartsWith("Finish"))
        {
            WinMenu.staticWinMenu.Victory();
        }
    }
}
