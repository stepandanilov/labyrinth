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

    public bool Visited = false;
    public int DistanceFromStart;
}
public class ThetaMazeGenerator
{
    public int radius = Globals.triangleMazeLength;

    public ThetaMazeCell[,] GenerateMaze()
    {
        int n = Globals.numberOfThetaCells;
        ThetaMazeCell[,] maze = new ThetaMazeCell[radius, radius + n];
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < x + n; y++)
            {
                maze[x, y] = new ThetaMazeCell
                {
                    indexX = x,
                    indexY = y,
                    r = (float)(x * 1.5),
                    angle = y * 2 * Mathf.PI / (Mathf.PI / ((float)180.0 / (x + n)))
                };
            }
        }
        RemoveWallsWithBacktracker(maze);
        
        RemoveOuterWalls(maze);

        return maze;
    }
    private void RemoveWallsWithBacktracker(ThetaMazeCell[,] maze)
    {
        ThetaMazeCell current = maze[0, 0];
        current.WallBottom = false;
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<ThetaMazeCell> stack = new Stack<ThetaMazeCell>();
        do
        {
            List<ThetaMazeCell> unvisitedNeighbours = new List<ThetaMazeCell>();

            int x = current.indexX;
            int y = current.indexY;

            List<ThetaMazeCell> neighbours = getNeighbours(maze, x, y);
            foreach(ThetaMazeCell cell in neighbours)
            {
                if (!cell.Visited) unvisitedNeighbours.Add(cell);
            }
            neighbours.Clear();

            if (unvisitedNeighbours.Count > 0)
            {
                ThetaMazeCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;
                current = chosen;
                stack.Push(chosen);
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);

    }
    private List<ThetaMazeCell> getNeighbours(ThetaMazeCell[,] maze, int x, int y)
    {
        List<ThetaMazeCell> neighbours = new List<ThetaMazeCell>();

        int n = Globals.numberOfThetaCells;
        //circle below
        if (x > 0)
        {
            for (int i = 0; i < x + n - 1; i++)
            {
                if ((maze[x, y].angle >= maze[x - 1, i].angle && maze[x, (y + 1) % (x + n - 2)].angle <= maze[x - 1, i].angle)
                    || (maze[x, y].angle <= maze[x - 1, i].angle && maze[x, (y + 1) % (x + n - 2)].angle >= maze[x - 1, i].angle))
                {
                    neighbours.Add(maze[x - 1, i]);
                }
            }
        }
        //same circle
        neighbours.Add(maze[x, (y + 1) % (x + n - 1)]);
        if (y > 0) neighbours.Add(maze[x, (y - 1) % (x + n - 1)]);
        else neighbours.Add(maze[x, x + n - 1]);
        //circle above
        if (x < Globals.triangleMazeLength - 2)
        {
            for (int i = 0; i < x + n + 1; i++)
            {
                if ((maze[x, y].angle >= maze[x + 1, i].angle && maze[x, (y + 1) % (x + n)].angle < maze[x + 1, i].angle)
                    || (maze[x, y].angle <= maze[x + 1, i].angle && maze[x, (y + 1) % (x + n)].angle > maze[x + 1, i].angle))
                {
                    neighbours.Add(maze[x + 1, i]);
                }
            }
        }

        return neighbours;
    }
    private void RemoveOuterWalls(ThetaMazeCell[,] maze)
    {
        for (int y = 0; y < maze.GetLength(1) - 1; y++)
        {
            maze[maze.GetLength(0) - 1, y].WallRight = false;
        }
    }
    private void RemoveWall(ThetaMazeCell a, ThetaMazeCell b)
    {
        if (a.indexX > b.indexX)
        {
            a.WallBottom = false;
        }
        else if (b.indexX > a.indexX)
        {
            b.WallBottom = false;
        }
        if (a.indexX == b.indexX)
        {
            if (a.indexY > b.indexY) a.WallRight = false;
            else b.WallRight = false;
        }
    }
}
