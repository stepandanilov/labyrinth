using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<int> path = new List<int>();
    private List<Vector2> pathToPoint = new List<Vector2>();
    private int framesToPoints = 15;
    public int indexX = 0;
    public int indexY = 0;
    private float triangleSide = 1.5f;
    //private int speed = 2;

    //public new Rigidbody2D rigidbody;

    private Vector2 offset;
    //private Vector2 destination;
    //private float positionReachedDistance = 0.01f;

    // not moving = 0
    // up - 1
    // right - 2
    // down - 3
    // left - 4 dead
    private int direction;
    // Start is called before the first frame update
    public void Start()
    {
        switch(PlayerPrefs.GetInt("type"))
        {
            case 1:
                transform.position = new Vector2(0.5f, 0.5f);
                offset = new Vector2(0.5f, 0.5f);
                break;
            case 2:
                transform.position = new Vector2(0.75f, 0.4330127f);
                offset = new Vector2(0.5f, (1f / Mathf.Sqrt(12)));
                offset *= triangleSide;
                break;
        }
    }
    public void StartBot()
    {
        //GameManager.getInstance().findPath();
        //path = GameManager.getInstance().path;
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                Utils.FindPath1();
                path = Utils.path1;
                break;
            case 2:
                Utils.FindPath1Delta();
                path = Utils.path1;
                break;
        }
        
        direction = path[0];
        path.RemoveAt(0);
        calcPathToPoint(direction);
    }
    public void StartBot2()
    {
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                Utils.FindPath2(new List<int>() { 1 }, 1, 0, 0);
                Utils.FindPath2(new List<int>() { 2 }, 2, 0, 0);
                Utils.FindPath2(new List<int>() { 3 }, 3, 0, 0);
                Utils.FindPath2(new List<int>() { 4 }, 4, 0, 0);
                break;
            case 2:
                Utils.FindPath2Delta(new List<int>() { 2 }, 2, 0, 0);
                Utils.FindPath2Delta(new List<int>() { 3 }, 3, 0, 0);
                Utils.FindPath2Delta(new List<int>() { 4 }, 3, 0, 0);
                break;
        }
        path = Utils.path2;
        path.RemoveAt(path.Count - 1);
        direction = path[0];
        path.RemoveAt(0);
        calcPathToPoint(direction);
    }
    private void Update()
    {
        if (pathToPoint.Count != 0)
        {
            transform.position = pathToPoint[0];
            pathToPoint.RemoveAt(0);
        }

        if (pathToPoint.Count == 0)
        {
            if (path.Count != 0)
            {
                direction = path[0];
                path.RemoveAt(0);
                calcPathToPoint(direction);
            }
        }
    }
    private void calcPathToPoint(int direction)
    {
        Vector2 currentPoint;
        Vector2 destinationPoint;
        pathToPoint.Clear();
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                currentPoint = new Vector2(indexX, indexY) + offset;
                destinationPoint = new Vector2();
                switch (direction)
                {
                    case 1:
                        indexY++;
                        destinationPoint = new Vector2(indexX, indexY) + offset;
                        break;
                    case 2:
                        indexX++;
                        destinationPoint = new Vector2(indexX, indexY) + offset;
                        break;
                    case 3:
                        indexY--;
                        destinationPoint = new Vector2(indexX, indexY) + offset;
                        break;
                    case 4:
                        indexX--;
                        destinationPoint = new Vector2(indexX, indexY) + offset;
                        break;
                }
                break;
            case 2:
                float x;
                float y;
                if (indexY % 2 == 0)
                {
                    x = (indexX + indexY / 4f) * triangleSide;
                    y = (indexY * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f;
                    currentPoint = new Vector2(x, y) + offset;
                }
                else
                {
                    x = (indexX + 1.25f + indexY / 4f) * triangleSide;
                    y = ((indexY + 1) * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f;
                    currentPoint = new Vector2(x, y) - offset;
                }
                destinationPoint = new Vector2();
                if (indexY % 2 == 0) //not rotated triangle
                {
                    switch (direction)
                    {
                        case 2://right
                            indexY++;
                            break;
                        case 3://down
                            indexY--;
                            break;
                        case 4://left
                            indexX--;
                            indexY++;
                            break;
                    }
                    destinationPoint = new Vector2((indexX + 1.25f + indexY / 4f) * triangleSide,
                                ((indexY + 1) * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f) - offset;
                }
                else //rotated triangle
                {
                    switch (direction)
                    {
                        case 2:
                            indexY--;
                            break;
                        case 3:
                            indexY++;
                            break;
                        case 4:
                            indexX++;
                            indexY--;
                            break;
                    }
                    destinationPoint = new Vector2((indexX + indexY / 4f) * triangleSide,
                        (indexY * Mathf.Sin(Mathf.PI / 3)) * triangleSide / 2f) + offset;
                }
                break;
            default:
                currentPoint.x = 0;
                currentPoint.y = 0;
                destinationPoint.x = 0;
                destinationPoint.y = 0;
                break;
        }
        float n;
        float m;
        float pointX;
        float pointY;
        for (int i = 0; i < framesToPoints; i++)
        {
            n = i;
            m = framesToPoints - 1 - i;

            pointX = currentPoint.x + (destinationPoint.x - currentPoint.x) / (n + m) * n;
            pointY = currentPoint.y + (destinationPoint.y - currentPoint.y) / (n + m) * n;

            pathToPoint.Add(new Vector2(pointX, pointY));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.getInstance().CollisionCheck(collision, transform);
    }
}
