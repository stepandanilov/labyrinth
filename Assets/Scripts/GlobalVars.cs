using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    public static MazeGeneratorCell[,] maze;
    private static int width = PlayerPrefs.GetInt("width") + 1;
    private static int height = PlayerPrefs.GetInt("height") + 1;
    public static List<int> path = new List<int>();
    public static int x = 0;
    public static int y = 0;
    public static int wallRightPosition = 3;
    private static bool turn = false;
    public static void findPath()
    {
        // not moving = 0
        // up - 1
        // right - 2
        // down - 3
        // left - 4
        if (!maze[x, y].IsFinishCell)
        {
            switch (wallRightPosition)
            {
                case 1:
                    if (maze[x, y + 1].WallBottom || turn)
                    {
                        if (x >= 0 && !maze[x, y].WallLeft)
                        {
                            path.Add(4);
                            Debug.Log(4);
                            x -= 1;
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
                    if (maze[x + 1, y].WallLeft || turn)
                    {
                        if (y <= height - 2 && !maze[x, y + 1].WallBottom)
                        {
                            path.Add(1);
                            Debug.Log(1);
                            y += 1;
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
                    if (maze[x, y].WallBottom || turn)
                    {
                        if (x <= width - 2 && !maze[x + 1, y].WallLeft)
                        {
                            path.Add(2);
                            Debug.Log(2);
                            x += 1;
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
                    if (maze[x, y].WallLeft || turn)
                    {
                        if (y >= 0 && !maze[x, y].WallBottom)
                        {
                            path.Add(3);
                            Debug.Log(3);
                            y -= 1;
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
            findPath();
        }
    }
}
