using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject FinishCellPrefab;
    public Transform Player;
    // Start is called before the first frame update
    public void Start()
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
}
