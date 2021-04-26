using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    //gamma maze
    public GameObject CellPrefab;
    public GameObject FinishCellPrefab;
    //delta
    public GameObject TriangleCellPrefab;
    public GameObject TriangleFinishCellPrefab;
    //theta
    public GameObject ThetaCellPrefab;

    public Transform Player;
    // Start is called before the first frame update
    public void Start()
    {
        //switch (Globals.mazeType)
        //{
        //    case 1:
        //        gammaMaze();
        //        break;
        //    case 2:
        //        deltaMaze();
        //        break;
        //}
        thetaMaze();
    }
    public void gammaMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();

                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);

                if (maze[x, y].IsFinishCell)
                {
                    if (x == 0)
                    {
                        FinishCell f = Instantiate(FinishCellPrefab, new Vector2(x - 1, y), Quaternion.identity).GetComponent<FinishCell>();

                        f.WallRight.SetActive(true);

                        f.WallBottom.SetActive(false);
                        f.WallTop.SetActive(false);
                        f.WallLeft.SetActive(false);
                    }
                    else if (y == 0)
                    {
                        FinishCell f = Instantiate(FinishCellPrefab, new Vector2(x, y - 1), Quaternion.identity).GetComponent<FinishCell>();

                        f.WallTop.SetActive(true);

                        f.WallBottom.SetActive(false);
                        f.WallRight.SetActive(false);
                        f.WallLeft.SetActive(false);
                    }
                    else if (y == maze.GetLength(1) - 1)
                    {
                        FinishCell f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                        f.WallBottom.SetActive(true);

                        f.WallRight.SetActive(false);
                        f.WallTop.SetActive(false);
                        f.WallLeft.SetActive(false);
                    }
                    else if (x == maze.GetLength(0) - 1)
                    {
                        FinishCell f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                        f.WallLeft.SetActive(true);

                        f.WallBottom.SetActive(false);
                        f.WallRight.SetActive(false);
                        f.WallTop.SetActive(false);
                    }
                }
            }
        }
    }
    public void deltaMaze()
    {
        TriangleMazeGenerator generator = new TriangleMazeGenerator();
        TriangleMazeGeneratorCell[,] maze = generator.GenerateMaze();
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1) - x; y++)
            {
                TriangleCell c = Instantiate(TriangleCellPrefab, new Vector2(maze[x, y].X, maze[x, y].Y), Quaternion.identity).GetComponent<TriangleCell>();

                c.WallBottom.SetActive(maze[x,y].WallBottom);
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallRight.SetActive(maze[x, y].WallRight);

                if (maze[x,y].isFinishCell)
                {
                    TriangleFinishCell f = Instantiate(TriangleFinishCellPrefab, new Vector2(maze[x, y].X, maze[x, y].Y), Quaternion.identity).GetComponent<TriangleFinishCell>();

                    if (x == 0)
                    {
                        f.WallLeft.SetActive(true);

                        f.WallBottom.SetActive(false);
                        f.WallRight.SetActive(false);
                    }
                    else if (y == 0)
                    {
                        f.WallBottom.SetActive(true);

                        f.WallLeft.SetActive(false);
                        f.WallRight.SetActive(false);
                    }
                    else
                    {
                        f.WallRight.SetActive(true);

                        f.WallLeft.SetActive(false);
                        f.WallBottom.SetActive(false);
                    }
                }
            }
        }
    }
    public void thetaMaze()
    {
        ThetaMazeGenerator generator = new ThetaMazeGenerator();
        ThetaMazeCell[,] maze = generator.GenerateMaze();
        
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                ThetaCell c = Instantiate(ThetaCellPrefab, Vector2.zero, Quaternion.identity).GetComponent<ThetaCell>();
                //adjust cell itself (rotation)
                c.transform.Rotate(new Vector3(0, 0, 1) * maze[x, y].angle / Mathf.PI * 180);
                //adjust circle walls (scale)
                c.WallBottom.transform.localScale = new Vector3( transform.localScale.x * (float)(1.5) * (x + 1),
                                                    transform.localScale.y * (float)(1.5) * (x + 1),
                                                    transform.localScale.z);
                //adjust inner walls to scale circle walls(scale)
                c.WallRight.transform.localScale = new Vector3(c.transform.localScale.x,
                                                    c.transform.localScale.y,
                                                    c.transform.localScale.z);
                //adjust position of inner walls (position)
                c.WallRight.transform.localPosition = new Vector3((float)(1.5 * x), 0, 0);

                c.WallBottom.SetActive(maze[x,y].WallBottom);
                c.WallRight.SetActive(maze[x, y].WallRight);
            }
        }
    }
}
