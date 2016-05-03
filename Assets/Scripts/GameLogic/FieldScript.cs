using UnityEngine;
using System.Collections;
using System;

public class FieldScript : MonoBehaviour
{

    // Use this for initialization
    public int m_LevelWidth;

    public GameObject m_EndTile;
    public GameObject m_BlankTile;
    public GameObject m_TowerTile;
    public GameObject m_StreetTile;
    public GameObject m_CurveTile;

    public int m_GeneratedRandomValue;

    private GameObject[][] m_TileArray;
    void Start()
    {
        m_TileArray = new GameObject[m_LevelWidth][];
        for (int i = 0; i < m_TileArray.Length; i++)
        {
            m_TileArray[i] = new GameObject[m_LevelWidth];
        }

        Init();
    }

    private void Init()
    {
        System.Random rand = new System.Random();
        double randValue = rand.NextDouble();
        int scenario = (int)(randValue * 100);

        m_GeneratedRandomValue = scenario;

        if(scenario < 33)
        {
            CreateScenario(0);
        }
        else if(scenario < 66)
        {
            CreateScenario(1);
        }
        else
        {
            CreateScenario(2);
        }
    }

    private void CreateScenario(int scenario)
    {

        int[,] tileOrder = GetTileValues(scenario);

        for (int x = 0; x < m_LevelWidth; x++)
        {
            for (int y = 0; y < m_LevelWidth; y++)
            {
                GameObject tile = null;
                switch(tileOrder[m_LevelWidth - y - 1, x])
                {
                    case 0:
                        tile = Instantiate(m_EndTile);
                        break;
                    case 1:
                        tile = Instantiate(m_BlankTile);
                        break;
                    case 2:
                        tile = Instantiate(m_TowerTile);
                        break;
                    case 3:
                        tile = Instantiate(m_StreetTile);
                        break;
                    case 4:
                        tile = Instantiate(m_CurveTile);
                        break;
                }
                Mesh mesh = tile.GetComponent<MeshFilter>().mesh;
                Vector3 size = mesh.bounds.size;
                tile.transform.position = new Vector3(x * size.x + size.x / 2.0f, -size.z / 2.0f, y * size.y + size.y / 2.0f);
                AddTileTo(tile, x, y);
            }
        }
    }

    private int[,] GetTileValues(int scenario)
    {
        if (scenario == 0)
        {
            int[,] tileOrder = new int[,]
            { { 1, 2, 3, 2, 1 },
              { 1, 1, 4, 4, 1 },
              { 1, 1, 2, 3, 2 },
              { 1, 1, 4, 4, 1 },
              { 1, 2, 3, 2, 1 }};
            return tileOrder;
        }
        else if(scenario == 1)
        {
            int[,] tileOrder = new int[,]
             { { 1, 2, 3, 2, 1 },
               { 1, 4, 4, 1, 1 },
               { 2, 3, 2, 1, 1 },
               { 1, 4, 4, 1, 1 },
               { 1, 2, 3, 2, 1 }};
            return tileOrder;
        }
        else
        {
            int[,] tileOrder = new int[,]
            { { 1, 2, 3, 2, 1 },
              { 1, 1, 3, 1, 1 },
              { 1, 2, 3, 2, 1 },
              { 1, 1, 3, 1, 1 },
              { 1, 2, 3, 2, 1 }};
            return tileOrder;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AddTileTo(GameObject go, int x, int y)
    {
        CheckArraySize(ref m_TileArray[x], y+1);
        if (IsValid(x, y))
        {
            m_TileArray[x][y] = go;
            //SingleRowAdvance(y); und checkarraysize kommentieren
            return true;
        }
        return false;
    }

    private void SingleRowAdvance(int y)
    {
        bool isFull = true;
        for (int i = 0; i < m_LevelWidth; i++)
        {
            if (m_TileArray[i][y] == null)
            {
                isFull = false;
                break;
            }
        }
        if (!isFull)
            return;
        for (int i = 0; i < m_LevelWidth; i++)
        {
            CheckArraySize(ref m_TileArray[i], y + 1);
        }
    }

    private void CheckArraySize(ref GameObject[] gameObject, int y)
    {
        if (gameObject.Length > y)
            return;

        GameObject[] newArray = new GameObject[y + 1];

        for (int i = 0; i < gameObject.Length; i++)
        {
            newArray[i] = gameObject[i];
        }
        gameObject = newArray;
    }

    /// <summary>
    /// Returns if the Tile [x][y] has a Neighbour
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool HasNeighbour(int x, int y)
    {
        if (IsValid(x, y))
        {
            if (x >= 0 && x < m_LevelWidth)
            {
                if (IsTile(x - 1, y))
                {
                    return true;
                }
                if (IsTile(x + 1, y))
                {
                    return true;
                }
                if (IsTile(x, y - 1))
                {
                    return true;
                }
                if (IsTile(x, y + 1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Returns if the Array exists
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsValid(int x, int y)
    {
        if (x >= 0 && x < m_LevelWidth)
        {
            if (y >= 0 && y < m_TileArray[x].Length)
            {
                return true;
            }
            if (x > 0)
            {
                if (y >= 0 && y < m_TileArray[x - 1].Length)
                {
                    return true;
                }
            }
            if (x < m_LevelWidth - 1)
            {
                if (y >= 0 && y < m_TileArray[x + 1].Length)
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Returns if the Tile is not null
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsTile(int x, int y)
    {
        if (x >= 0 && x < m_LevelWidth)
        {
            if (y >= 0 && y < m_TileArray[x].Length)
            {
                return m_TileArray[x][y] != null;
            }
        }
        return false;
    }

    public GameObject GetTileFrom(int x, int y)
    {
        try
        {
            return m_TileArray[x][y];
        }
        catch
        {
            return null;
        }
    }
}
