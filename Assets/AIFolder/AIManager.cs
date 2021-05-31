using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<int> path = new List<int>();
    private List<Vector2> pathToPoint = new List<Vector2>();
    private int framesToPoints = 30;
    private int indexX = 0;
    private int indexY = 0;
    //private int speed = 2;

    //public new Rigidbody2D rigidbody;

    private Vector2 offset = new Vector2(0.5f, 0.5f);
    //private Vector2 destination;
    //private float positionReachedDistance = 0.01f;

    // not moving = 0
    // up - 1
    // right - 2
    // down - 3
    // left - 4 dead
    private int state;
    // Start is called before the first frame update
    public void StartBot()
    {
        //GameManager.getInstance().findPath();
        //path = GameManager.getInstance().path;
        GlobalVars.FindPath1();
        path = GlobalVars.path1;
        state = path[0];
        path.RemoveAt(0);
        calcPathToPoint(state);
    }
    public void StartBot2()
    {
        GlobalVars.FindPath2(new List<int>() { 1 }, 1, 0, 0);
        GlobalVars.FindPath2(new List<int>() { 2 }, 2, 0, 0);
        GlobalVars.FindPath2(new List<int>() { 3 }, 3, 0, 0);
        GlobalVars.FindPath2(new List<int>() { 4 }, 4, 0, 0);

        path = GlobalVars.path2;
        path.RemoveAt(path.Count - 1);
        state = path[0];
        path.RemoveAt(0);
        calcPathToPoint(state);
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
                state = path[0];
                path.RemoveAt(0);
                calcPathToPoint(state);
            }
        }
        

        //switch (state)
        //{
        //    case 0:
        //        rigidbody.velocity = new Vector2(0, 0);
        //        break;
        //    case 1:
        //        rigidbody.velocity = new Vector2(0, speed);
        //        break;
        //    case 2:
        //        rigidbody.velocity = new Vector2(speed, 0);
        //        break;
        //    case 3:
        //        rigidbody.velocity = new Vector2(0, -speed);
        //        break;
        //    case 4:
        //        rigidbody.velocity = new Vector2(-speed, 0);
        //        break;
        //}
        //if (Vector2.Distance(transform.position, destination) < positionReachedDistance)
        //{
        //    if (path.Count > 0)
        //    {
        //        state = path[0];
        //        path.RemoveAt(0);
        //        move(state);
        //    }
        //    else
        //    {
        //        state = 0;
        //    }
        //}

    }
    //private void move(int direction)
    //{
    //    switch (direction)
    //    {
    //        case 1:
    //            indexY++;
    //            destination = new Vector2(indexX, indexY) + offset;
    //            break;
    //        case 2:
    //            indexX++;
    //            destination = new Vector2(indexX, indexY) + offset;
    //            break;
    //        case 3:
    //            indexY--;
    //            destination = new Vector2(indexX, indexY) + offset;
    //            break;
    //        case 4:
    //            indexX--;
    //            destination = new Vector2(indexX, indexY) + offset;
    //            break;
    //    }
    //}
    private void calcPathToPoint(int direction)
    {
        pathToPoint.Clear();
        Vector2 currentPoint = new Vector2(indexX, indexY) + offset;
        Vector2 destinationPoint = new Vector2();
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
}
