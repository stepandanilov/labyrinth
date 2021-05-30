using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public List<int> path = new List<int>();
    //gamma
    private static int width = PlayerPrefs.GetInt("width") + 1;
    private static int height = PlayerPrefs.GetInt("height") + 1;
    public MazeGeneratorCell[,] maze;
    private int wallRightPosition = 3;
    private int x = 0;
    private int y = 0;

    private GameManager() { }
    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }
    public int getNumberOfCellsInRow(int x)
    {
        int cells = PlayerPrefs.GetInt("cellNumber") / 4;

        while (x + 1 >= cells)
        {
            cells *= 2;
        }

        return cells * 4;
    }
    public void setMaze(MazeGeneratorCell[,] maze)
    {
        this.maze = maze;
    }
    public MazeGeneratorCell[,] getMaze()
    {
        return maze;
    }
    public void printTest()
    {
        maze = GlobalVars.maze;
    }
    public void findPath()
    {
        // not moving = 0
        // up - 1
        // right - 2
        // down - 3
        // left - 4
        if (!maze[x, y].IsFinishCell)
            switch (wallRightPosition)
            {
                case 1:
                    if (x > 0 && maze[x, y].WallLeft)
                    {
                        path.Add(4);
                        findPath();
                    }
                    else
                    {
                        wallRightPosition = 4;
                        findPath();
                    }
                    break;
                case 2:
                    if (y < height - 2 && maze[x, y + 1].WallBottom)
                    {
                        path.Add(1);
                        findPath();
                    }
                    else
                    {
                        wallRightPosition = 1;
                        findPath();
                    }
                    break;
                case 3:
                    if (x < width - 2 && maze[x + 1, y].WallLeft)
                    {
                        path.Add(2);
                        findPath();
                    }
                    else
                    {
                        wallRightPosition = 2;
                        findPath();
                    }
                    break;
                case 4:
                    if (y > 0 && maze[x, y].WallBottom)
                    {
                        path.Add(3);
                        findPath();
                    }
                    else
                    {
                        wallRightPosition = 3;
                        findPath();
                    }
                    break;
            }
        GlobalVars.path = path;
    }
}
