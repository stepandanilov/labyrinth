using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThetaMazeCell
{
    //index in array
    public int indexX;
    public int indexY;
    //polar coordinates
    public float r;
    public float angle;

    public bool WallBottom = true;
    public bool WallRight = true;

}
public class ThetaMazeGenerator
{
    public int radius = Globals.triangleMazeLength;

    public ThetaMazeCell[,] GenerateMaze()
    {
        ThetaMazeCell[,] maze = new ThetaMazeCell[radius, 6];
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new ThetaMazeCell
                {
                    indexX = x,
                    indexY = y,
                    r = (float)(x * 1.5),
                    angle = y * Mathf.PI / 3
                };
            }
        }

        RemoveOuterWalls(maze);

        return maze;
    }
    private void RemoveOuterWalls(ThetaMazeCell[,] maze)
    {
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[maze.GetLength(0) - 1, y].WallRight = false;
        }
    }
}
