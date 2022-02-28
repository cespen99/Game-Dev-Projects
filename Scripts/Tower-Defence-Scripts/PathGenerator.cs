using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject towerSquare, enemySquare;
    [SerializeField] private int xLen, yLen;
    private int[,] grid; // 0-empty 1-path 2-tower
    public static List<GameObject> path;

    void Awake()
    {
        grid = new int[25, 15];
        path = new List<GameObject>();
        CreateGrid();
        SetTower();
    }

    private void CreateGrid()
    {
        int nextMove, prevMove = -1;
        int x = 0;
        int y = (Random.Range(0, 14));
        SetPath(x, y);

        while (x < 23)
        {
            nextMove = Random.Range(0, 3);
            while((nextMove == 0 && prevMove == 1) || (nextMove == 1 && prevMove == 0))
                nextMove = Random.Range(0, 3);
            prevMove = nextMove;
            switch (nextMove)
            {
                case 0:
                    if (y < 13)
                        y++;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 1:
                    if (y > 0)
                        y--;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 2:
                    x++;
                    break;
            }
            SetPath(x, y);
        }
    }

    private void SetPath(int x, int y)
    {
        grid[x, y] = 1;
        GameObject spot = Instantiate(enemySquare, new Vector3((x+1)*2, (y+1)*2, 0.75f), Quaternion.Euler(90, 0, 0));
        path.Add(spot);
        path[path.Count - 1].name = (path.Count - 1).ToString();
    }

    private void SetTower()
    {
        for (int x = 1; x < 24; x++)
        {
            for (int y = 0; y < 14; y++)
            {
                if (grid[x, y] == 0 && CanPlaceTower(x, y))
                {
                    grid[x, y] = 2;
                    Instantiate(towerSquare, new Vector3((x + 1) * 2, (y + 1) * 2, 0.75f), Quaternion.identity);
                }
            }
        }
    }

    private bool CanPlaceTower(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int a = x + i;
                int b = y + j;
                if (a >= 0 && a < 25 && b >= 0 && b < 15 && grid[a, b] == 1)
                    return true;
            }
        }
        return false;
    }

}
