using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMazeGeneratorCell
{
    public int indexX;
    public int indexY;

    public float X;
    public float Y;

    public bool WallLeft = true;
    public bool WallRight = true;
    public bool WallBottom = true;
    
    public bool Visited = false;
    public int DistanceFromStart;

    public bool isFinishCell = false;
}
public class TriangleMazeGenerator
{
    public int mazeLength = Globals.triangleMazeLength;

    public TriangleMazeGeneratorCell[,] GenerateMaze()
    {
        TriangleMazeGeneratorCell[,] maze = new TriangleMazeGeneratorCell[mazeLength, mazeLength];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1) - x; y++)
            {
                maze[x, y] = new TriangleMazeGeneratorCell
                {
                    X = (float)((x + y * 0.5) * 1.5),
                    Y = (float)(y * Mathf.Sin(Mathf.PI / 3) * 1.5),
                    indexX = x,
                    indexY = y
                };
            }
        }
        removeWallsWithBacktracker(maze);

        cleanUp(maze);

        placeExit(maze);

        return maze;
    }
    private void removeWallsWithBacktracker(TriangleMazeGeneratorCell[,] maze)
    {
        TriangleMazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<TriangleMazeGeneratorCell> stack = new Stack<TriangleMazeGeneratorCell>();
        do
        {
            List<TriangleMazeGeneratorCell> unvisitedNeighbours = new List<TriangleMazeGeneratorCell>();

            int x = current.indexX;
            int y = current.indexY;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]); //1
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]); //6
            if (x < mazeLength - 1 - y && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]); //4
            if (y < mazeLength - 1 - x && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]); //3
            if (y < mazeLength - 1 - x && x > 0 && !maze[x - 1, y + 1].Visited) unvisitedNeighbours.Add(maze[x - 1, y + 1]); //2
            if (x < mazeLength - 1 - y && y > 0 && !maze[x + 1, y - 1].Visited) unvisitedNeighbours.Add(maze[x + 1, y - 1]); //5

            if (unvisitedNeighbours.Count > 0)
            {
                TriangleMazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
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
    private void RemoveWall(TriangleMazeGeneratorCell a, TriangleMazeGeneratorCell b)
    {
        if (a.indexY == b.indexY)
        {
            if (a.indexX > b.indexX)
            {
                a.WallLeft = false;
                b.WallRight = false;
            }
            else
            {
                a.WallRight = false;
                b.WallLeft = false;
            }
        }
        else if (a.indexX == b.indexX)
        {
            if (a.indexY > b.indexY)
            {
                a.WallBottom = false;
                b.WallRight = false;
            }
            else
            {
                a.WallRight = false;
                b.WallBottom = false;
            }
        }
        else
        //if (a.indexX + a.indexY == b.indexY + b.indexY)
        {
            Debug.Log("(" + a.indexX + ";" + a.indexY + ") (" + b.indexX + ";" + b.indexY + ")");
            if (a.indexX > b.indexX)
            {
                a.WallLeft = false;
                b.WallBottom = false;
            }
            else
            {
                a.WallBottom = false;
                b.WallLeft = false;
            }
        }
    }
    private void cleanUp(TriangleMazeGeneratorCell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < maze.GetLength(1) - x -1; y++)
            {
                if (maze[x, y].WallRight && maze[x + 1, y].WallLeft && maze[x, y + 1].WallBottom) 
                {
                    int rand = UnityEngine.Random.Range(0,2);
                    switch(rand)
                    {
                        case 0:
                            maze[x, y].WallRight = false;
                            break;
                        case 1:
                            maze[x + 1, y].WallLeft = false;
                            break;
                        case 2:
                            maze[x, y + 1].WallBottom = false;
                            break;
                    }
                }
            }
        }
    }
    private void placeExit(TriangleMazeGeneratorCell[,] maze)
    {
        //finding furthest cell
        TriangleMazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++) 
        {
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, mazeLength - 1 - x].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, mazeLength - 1 - x];
        }
        furthest.isFinishCell = true;
        //determining which side it's on
        if (furthest.indexX == 0)
        {
            furthest.WallLeft = false;
        }
        else if (furthest.indexY == 0)
        {
            furthest.WallBottom = false;
        }
        else
        {
            furthest.WallRight = false;
        }
    }
}
