using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static MazeGeneratorCell[,] maze;
    public static TriangleMazeGeneratorCell[,] deltaMaze;
    private static readonly int width = PlayerPrefs.GetInt("width") + 1;
    private static readonly int height = PlayerPrefs.GetInt("height") + 1;

    //AI - 1
    public static List<int> path1 = new List<int>();
    public static int x1 = 0;
    public static int y1 = 0;
    public static int wallRightPosition = 3;
    public static int wallRightPositionRotated = 2;
    public static bool change = true;
    private static bool turn = false;

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
        if (!maze[x1, y1].IsFinishCell)
        {
            switch (wallRightPosition)
            {
                case 1:
                    if (maze[x1, y1 + 1].WallBottom || turn)
                    {
                        if (x1 >= 0 && !maze[x1, y1].WallLeft)
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
                    if (maze[x1 + 1, y1].WallLeft || turn)
                    {
                        if (y1 <= height - 2 && !maze[x1, y1 + 1].WallBottom)
                        {
                            path1.Add(1);
                            y1 += 1;
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
                    if (maze[x1, y1].WallBottom || turn)
                    {
                        if (x1 <= width - 2 && !maze[x1 + 1, y1].WallLeft)
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
                    if (maze[x1, y1].WallLeft || turn)
                    {
                        if (y1 >= 0 && !maze[x1, y1].WallBottom)
                        {
                            path1.Add(3);
                            y1 -= 1;
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
    public static void FindPath1Delta()
    {
        // not moving = 0
        // up - 1
        // right - 2
        // down - 3
        // left - 4
        if (!deltaMaze[x1, y1].isFinishCell)
        {
            if (y1 % 2 == 0) //not rotated cell
            {
                if (change)
                {
                    wallRightPosition = wallRightPositionRotated + 1;
                    if (wallRightPosition == 5) wallRightPosition = 2;
                }

                switch (wallRightPosition)
                {
                    case 2:
                        if (deltaMaze[x1, y1].WallRight || turn)
                        {
                            if ((x1 > 0) && !deltaMaze[x1, y1].WallLeft 
                                && !deltaMaze[x1 - 1, y1 + 1].WallLeft)
                            {
                                path1.Add(4);
                                x1--;
                                y1++;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPosition = 4;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPosition = 3;
                            turn = true;
                            change = false;
                        }
                        break;
                    case 3:
                        if (deltaMaze[x1,y1].WallBottom || turn)
                        {
                            if ((y1 < deltaMaze.GetLength(0) * 2 - x1 * 2 - 1)
                                && !deltaMaze[x1, y1].WallRight
                                && !deltaMaze[x1, y1 + 1].WallRight)
                            {
                                path1.Add(2);
                                y1++;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPosition = 2;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPosition = 4;
                            turn = true;
                            change = false;
                        }
                        break;
                    case 4:
                        if (deltaMaze[x1, y1].WallRight || turn)
                        {
                            if ((y1 > 0) && !deltaMaze[x1, y1].WallBottom
                                && !deltaMaze[x1, y1 - 1].WallBottom)
                            {
                                path1.Add(3);
                                y1--;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPosition = 3;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPosition = 2;
                            turn = true;
                            change = false;
                        }
                        break;
                }
                
            }
            else //rotated cell
            {
                if (change)
                {
                    wallRightPositionRotated = wallRightPosition + 1;
                    if (wallRightPositionRotated == 5) wallRightPositionRotated = 2;
                }
                
                switch (wallRightPositionRotated)
                {
                    case 2:
                        if (deltaMaze[x1, y1].WallRight || turn)
                        {
                            if (!deltaMaze[x1, y1].WallLeft && !deltaMaze[x1 + 1, y1 - 1].WallLeft)
                            {
                                path1.Add(4);
                                x1++;
                                y1--;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPositionRotated = 4;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPositionRotated = 3;
                            turn = true;
                            change = false;
                        }
                        break;
                    case 3:
                        if (deltaMaze[x1, y1].WallBottom || turn)
                        {
                            if (!deltaMaze[x1, y1].WallRight && !deltaMaze[x1, y1 - 1].WallRight)
                            {
                                path1.Add(2);
                                y1--;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPositionRotated = 2;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPositionRotated = 4;
                            turn = true;
                            change = false;
                        }
                        break;
                    case 4:
                        if (deltaMaze[x1, y1].WallLeft || turn)
                        {
                            if (!deltaMaze[x1, y1].WallBottom && !deltaMaze[x1, y1 + 1].WallBottom)
                            {
                                path1.Add(3);
                                y1++;
                                turn = false;
                                change = true;
                            }
                            else
                            {
                                wallRightPositionRotated = 3;
                                change = false;
                            }
                        }
                        else
                        {
                            wallRightPositionRotated = 2;
                            turn = true;
                            change = false;
                        }
                        break;
                }
            }
            FindPath1Delta();
        }
    }
    public static void FindPath2Delta(List<int> path, int direction, int x, int y)
    {
        if (pathNotFound)
        {
            if (deltaMaze[x, y].isFinishCell)
            {
                path2 = new List<int>(path);
                pathNotFound = false;
            }
            else
            {
                if (y % 2 == 0) //not rotated cell
                {
                    switch (direction)
                    {
                        case 2:
                            if (!deltaMaze[x, y].WallRight)
                            {
                                TryThisWay(path, 3, x, y + 1);
                                TryThisWay(path, 4, x, y + 1);
                            }
                            break;
                        case 3:
                            if (!deltaMaze[x, y].WallBottom)
                            {
                                TryThisWay(path, 2, x, y - 1);
                                TryThisWay(path, 4, x, y - 1);
                            }
                            break;
                        case 4:
                            if (!deltaMaze[x, y].WallLeft)
                            {
                                TryThisWay(path, 2, x - 1, y + 1);
                                TryThisWay(path, 3, x - 1, y + 1);
                            }
                            break;
                    }
                }
                else //rotated cell
                {
                    switch (direction)
                    {
                        case 2:
                            if (!deltaMaze[x, y].WallRight)
                            {
                                TryThisWay(path, 3, x, y - 1);
                                TryThisWay(path, 4, x, y - 1);
                            }
                            break;
                        case 3:
                            if (!deltaMaze[x, y].WallBottom)
                            {
                                TryThisWay(path, 2, x, y + 1);
                                TryThisWay(path, 4, x, y + 1);
                            }
                            break;
                        case 4:
                            if (!deltaMaze[x, y].WallLeft)
                            {
                                TryThisWay(path, 2, x + 1, y - 1);
                                TryThisWay(path, 3, x + 1, y - 1);
                            }
                            break;
                    }
                }
            }
        }
    }
    public static void TryThisWay(List<int> path, int direction, int x, int y)
    {
        path.Add(direction);
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                FindPath2(path, direction, x, y);
                break;
            case 2:
                FindPath2Delta(path, direction, x, y);
                break;
        }
        path.RemoveAt(path.Count - 1);
    }
    public static int getNumberOfCellsInRow(int x)
    {
        int cells = PlayerPrefs.GetInt("cellNumber") / 4;

        while (x + 1 >= cells)
        {
            cells *= 2;
        }

        return cells * 4;
    }
}
