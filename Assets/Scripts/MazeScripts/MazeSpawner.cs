﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Material material;
    public Material finishMaterial;

    //gamma maze
    public GameObject CellPrefab;
    public GameObject FinishCellPrefab;
    //delta
    public GameObject TriangleCellPrefab;
    public GameObject TriangleFinishCellPrefab;
    //theta
    public GameObject ThetaCellPrefab;
    public GameObject ThetaFinishCellPrefab;
    //nefu
    public GameObject NefuMazePrefab;
    public Transform Player;
    // Start is called before the first frame update
    public void Awake()
    {
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                gammaMaze();
                break;
            case 2:
                deltaMaze();
                break;
            case 3:
                thetaMaze();
                break;
            case 4:
                nefuMaze();
                break;
        }
    }
    public void gammaMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();
        Utils.maze = maze;
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();

                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);

                if (maze[x, y].IsFinishCell)
                {
                    FinishCell f;
                    switch (Utils.finishWallDirection)
                    {
                        case 1:
                            f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                            f.WallTop.SetActive(true);

                            f.WallBottom.SetActive(false);
                            f.WallLeft.SetActive(false);
                            f.WallRight.SetActive(false);
                            break;
                        case 2:
                            f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                            f.WallRight.SetActive(true);

                            f.WallTop.SetActive(false);
                            f.WallBottom.SetActive(false);
                            f.WallLeft.SetActive(false);
                            break;
                        case 3:
                            f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                            f.WallBottom.SetActive(true);

                            f.WallTop.SetActive(false);
                            f.WallRight.SetActive(false);
                            f.WallLeft.SetActive(false);
                            break;
                        case 4:
                            f = Instantiate(FinishCellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<FinishCell>();

                            f.WallLeft.SetActive(true);

                            f.WallBottom.SetActive(false);
                            f.WallTop.SetActive(false);
                            f.WallRight.SetActive(false);
                            break;
                    }
                }
            }
        }
    }
    public void deltaMaze()
    {
        TriangleMazeGenerator generator = new TriangleMazeGenerator();
        TriangleMazeGeneratorCell[,] maze = generator.GenerateMaze();
        Utils.deltaMaze = maze;
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(0) * 2 - x * 2 - 1; y++)
            {
                TriangleCell c = Instantiate(TriangleCellPrefab, new Vector2(maze[x, y].X, maze[x, y].Y), Quaternion.identity).GetComponent<TriangleCell>();

                c.WallBottom.SetActive(maze[x,y].WallBottom);
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallRight.SetActive(maze[x, y].WallRight);

                if (y % 2 == 1) c.transform.Rotate(Vector3.forward, 180);

                if (maze[x,y].isFinishCell)
                {
                    TriangleFinishCell f = Instantiate(TriangleFinishCellPrefab, new Vector2(maze[x, y].X, maze[x, y].Y), Quaternion.identity).GetComponent<TriangleFinishCell>();
                    switch(Utils.finishWallDirection)
                    {
                        case 2:
                            f.WallRight.SetActive(true);

                            f.WallLeft.SetActive(false);
                            f.WallBottom.SetActive(false);
                            break;
                        case 3:
                            f.WallBottom.SetActive(true);

                            f.WallLeft.SetActive(false);
                            f.WallRight.SetActive(false);
                            break;
                        case 4:
                            f.WallLeft.SetActive(true);

                            f.WallBottom.SetActive(false);
                            f.WallRight.SetActive(false);
                            break;
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
            for (int y = 0; y < Utils.getNumberOfCellsInRow(x) ; y++) 
            {
                ThetaCell c = Instantiate(ThetaCellPrefab, Vector2.zero, Quaternion.identity).GetComponent<ThetaCell>();
                MakeThetaCellBottomWall(c.gameObject, x, "WallBottom");

                c.x = x;
                c.y = y;
                c.distanceFromStart = maze[x, y].DistanceFromStart;
                c.visited = maze[x, y].Visited;
                //adjust cell itself (rotation)
                c.transform.Rotate(Vector3.forward, maze[x, y].angle);
                //adjust circle walls (scale)
                c.WallBottom.transform.localScale = new Vector3(transform.localScale.x * (float)(1.5) * (x + 1),
                                                    transform.localScale.y * (float)(1.5) * (x + 1),
                                                    0);
                //adjust inner walls to scale circle walls(scale)
                c.WallRight.transform.localScale = new Vector3(c.transform.localScale.x,
                                                    c.transform.localScale.y,
                                                    0);
                //adjust position of inner walls (position)
                c.WallRight.transform.localPosition = new Vector3((float)(1.5 * x), 0, 0);

                c.WallBottom.SetActive(maze[x, y].WallBottom);
                c.WallRight.SetActive(maze[x, y].WallRight);

                if (maze[x,y].isFinishCell)
                {
                    ThetaFinishCell f = Instantiate(ThetaFinishCellPrefab, Vector2.zero, Quaternion.identity).GetComponent<ThetaFinishCell>();
                    //make and change bottom wall
                    MakeThetaCellBottomWall(f.gameObject, x, "FinishWallBottom");
                    LineRenderer lineRenderer = f.gameObject.transform.Find("FinishWallBottom").gameObject.GetComponent<LineRenderer>();
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.green;

                    lineRenderer.material = finishMaterial;
                    //adjusting
                    f.transform.Rotate(Vector3.forward, maze[x, y].angle);
                    f.WallBottom.transform.localScale = new Vector3(transform.localScale.x * (float)(1.5) * (x + 1),
                                                    transform.localScale.y * (float)(1.5) * (x + 1),
                                                    0);
                    f.WallBottom.SetActive(true);
                }
            }
        }
    }
    public void MakeThetaCellBottomWall(GameObject thetaCell, int x, string childName)
    {
        //adding linerenderer
        LineRenderer lineRenderer = thetaCell.transform.Find(childName).gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = 8;

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.material = material;

        lineRenderer.useWorldSpace = false;
        var points = new Vector3[lineRenderer.positionCount];

        int n = PlayerPrefs.GetInt("cellNumber");
        float angle =((float)360.0 / Utils.getNumberOfCellsInRow(x)) / (float)(lineRenderer.positionCount - 1);
        for (int i = 0; i < lineRenderer.positionCount; i++) 
        {
            points[i] = new Vector3((Mathf.Cos(angle * Mathf.PI / (float)180.0 * i)),
                                    (Mathf.Sin(angle * Mathf.PI / (float)180.0 * i)), 0);
        }
        lineRenderer.SetPositions(points);
        //adding edgecollider
        EdgeCollider2D edgeCollider = thetaCell.transform.Find(childName).gameObject.AddComponent<EdgeCollider2D>();

        var colliderPoints = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            colliderPoints[i].x = points[i].x;
            colliderPoints[i].y = points[i].y;
        } 

        edgeCollider.points = colliderPoints;
    }
    public void nefuMaze()
    {
        Transform maze = Instantiate(NefuMazePrefab, Vector2.zero, Quaternion.identity).GetComponent<Transform>();
        foreach(Transform child in maze)
        {
            if (child.gameObject.activeSelf)
            {
                LineRenderer lineRenderer = child.gameObject.GetComponent<LineRenderer>();
                Vector3[] points = new Vector3[lineRenderer.positionCount];
                lineRenderer.GetPositions(points);

                EdgeCollider2D edgeCollider = child.gameObject.AddComponent<EdgeCollider2D>();
                var colliderPoints = new Vector2[lineRenderer.positionCount];
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    if (lineRenderer.useWorldSpace)
                    {
                        child.transform.position = Vector3.zero;
                    }
                    colliderPoints[i].x = points[i].x;
                    colliderPoints[i].y = points[i].y;
                }
                edgeCollider.points = colliderPoints;
                Debug.Log(child.gameObject.GetComponent<LineRenderer>().positionCount);
            }
        }
    }
}
