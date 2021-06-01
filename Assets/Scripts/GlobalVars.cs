using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    public static MazeGeneratorCell[,] maze;
    private static readonly int width = PlayerPrefs.GetInt("width") + 1;
    private static readonly int height = PlayerPrefs.GetInt("height") + 1;

    //AI - 1
    public static List<int> path1 = new List<int>();
    public static int x1 = 0;
    public static int y1 = 0;
    //gamma
    public static int wallRightPosition = 3;
    private static bool turn = false;
    //delta


    //AI - 2
    public static List<int> path2;
    public static int x2 = 0;
    public static int y2 = 0;
    public static bool pathNotFound = true;
    public static void FindPath1()
    {
        // not moving = 0
        // up - 1
        // right - 2
        // down - 3
        // left - 4
        if (!maze[x1, y2].IsFinishCell)
        {
            switch (wallRightPosition)
            {
                case 1:
                    if (maze[x1, y2 + 1].WallBottom || turn)
                    {
                        if (x1 >= 0 && !maze[x1, y2].WallLeft)
                        {
                            path1.Add(4);
                            x1 -= 1;
                            turn = false;
                        }
                        else
                        {
                            wallRightPosition = 4;
                            turn = false;
                        }
                    }
                    else
                    {
                        wallRightPosition = 2;
                        turn = true;
                    }
                    break;
                case 2:
                    if (maze[x1 + 1, y2].WallLeft || turn)
                    {
                        if (y2 <= height - 2 && !maze[x1, y2 + 1].WallBottom)
                        {
                            path1.Add(1);
                            y2 += 1;
                            turn = false;
                        }
                        else
                        {
                            turn = false;
                            wallRightPosition = 1;
                        }
                    }
                    else
                    {
                        wallRightPosition = 3;
                        turn = true;
                    }
                    break;
                case 3:
                    if (maze[x1, y2].WallBottom || turn)
                    {
                        if (x1 <= width - 2 && !maze[x1 + 1, y2].WallLeft)
                        {
                            path1.Add(2);
                            x1 += 1;
                            turn = false;
                        }
                        else
                        {
                            wallRightPosition = 2;
                            turn = false;
                        }
                    }
                    else
                    {
                        wallRightPosition = 4;
                        turn = true;
                    }
                    break;
                case 4:
                    if (maze[x1, y2].WallLeft || turn)
                    {
                        if (y2 >= 0 && !maze[x1, y2].WallBottom)
                        {
                            path1.Add(3);
                            y2 -= 1;
                            turn = false;
                        }
                        else
                        {
                            wallRightPosition = 3;
                            turn = false;
                        }
                    }
                    else
                    {
                        wallRightPosition = 1;
                        turn = true;
                    }
                    break;
            }
            FindPath1();
        }
    }
    public static void FindPath2(List<int> path, int direction, int x, int y)
    {
        // up - 1
        // right - 2
        // down - 3
        // left - 4
        if (pathNotFound)
        {
            if (maze[x, y].IsFinishCell)
            {
                path2 = new List<int>(path);
                pathNotFound = false;
            }
            else if (y <= height - 2 && direction == 1 && !maze[x, y + 1].WallBottom)
            {
                TryThisWay(path, 1, x, y + 1);
                TryThisWay(path, 2, x, y + 1);
                TryThisWay(path, 4, x, y + 1);
            }
            else if (x <= width - 2 && direction == 2 && !maze[x + 1, y].WallLeft)
            {
                TryThisWay(path, 2, x + 1, y);
                TryThisWay(path, 1, x + 1, y);
                TryThisWay(path, 3, x + 1, y);
            }
            else if (y >= 0 && direction == 3 && !maze[x, y].WallBottom)
            {
                TryThisWay(path, 3, x, y - 1);
                TryThisWay(path, 2, x, y - 1);
                TryThisWay(path, 4, x, y - 1);
            }
            else if (x >= 0 && direction == 4 && !maze[x, y].WallLeft)
            {
                TryThisWay(path, 4, x - 1, y);
                TryThisWay(path, 3, x - 1, y);
                TryThisWay(path, 1, x - 1, y);
            }
        }
    }
    public static void TryThisWay(List<int> path, int direction, int x, int y)
    {
        path.Add(direction);
        FindPath2(path, direction, x, y);
        path.RemoveAt(path.Count - 1);
    }
    public static void FindPath1Delta()
    {

    }
}
