using UnityEngine;
using System.Collections;
using System;

public class FieldScript : MonoBehaviour
{

    // Use this for initialization
    public int m_LevelWidth;

    // system to get all possible tiles;
    public GameObject m_TileSystem;

    public float m_SizeX;
    public float m_SizeY;

    //to see which number was generated
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
        int scenario = (int)(randValue * 99);

        m_GeneratedRandomValue = scenario;

        CreateScenario(scenario/33);
    }

    private void CreateScenario(int scenario)
    {

        int[,] tileOrder = GetTileValues(scenario);

        for (int x = 0; x < m_LevelWidth; x++)
        {
            for (int y = 0; y < m_LevelWidth; y++)
            {
                GameObject tile = null;

                int tileVariationValue = tileOrder[m_LevelWidth - y - 1, x] / 10;
                int tileRotationValue = tileOrder[m_LevelWidth - y - 1, x] % 10;

                switch(tileVariationValue)
                {
                    case 11:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_BlankVar1);
                        break;
                    case 12:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_BlankVar2);
                        break;
                    case 21:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_TowerBuildSpotVar1);
                        break;
                    case 22:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_TowerBuildSpotVar2);
                        break;
                    case 31:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetStraightVar1);
                        break;
                    case 32:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetStraightVar2);
                        break;
                    case 41:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetCurveVar1);
                        break;
                    case 42:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetCurveVar2);
                        break;
                    case 51:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetEndVar1);
                        break;
                    case 52:
                        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetEndVar2);
                        break;
                }

                //Mesh mesh = tile.GetComponent<MeshFilter>().mesh;
                //Vector3 size = mesh.bounds.size;
                //drehen
                PathTileScript path = null;
                if (tileRotationValue == 2)
                {
                    tile.transform.Rotate(Vector3.forward, 90);
                    path = tile.GetComponent<PathTileScript>();
                    if(path != null)
                        path.Rotate();
                }
                else if (tileRotationValue == 3)
                {
                    tile.transform.Rotate(Vector3.forward, 180);
                    path = tile.GetComponent<PathTileScript>();
                    if (path != null)
                    {
                        path.Rotate();
                        path.Rotate();
                    }
                }
                else if (tileRotationValue == 4)
                {
                    tile.transform.Rotate(Vector3.forward, 270);
                    path = tile.GetComponent<PathTileScript>();
                    if (path != null)
                    {
                        path.Rotate();
                        path.Rotate();
                        path.Rotate();
                    }
                }
                tile.transform.position = new Vector3(x * m_SizeX + m_SizeX / 2.0f, 0.0f, y * m_SizeY + m_SizeY / 2.0f);
                AddTileTo(tile, x, y);
            }
        }
    }

    //code for tiles:
    //first value: category of tile (blank, street, buildPlace)...
    //second value: rotation
    //third value: variation ID
    /*
     *  First:
     *  1 blank
     *  2 tower
     *  3 street straight
     *  4 street curve
     *  5 end of street
     *  
     *  Second:
     *  1 var1
     *  2 var2
     *  
     *  Third:
     *  1 0 degree
     *  2 90 degree
     *  3 180 degree
     *  4 270 degree
     * 
     */
    private int[,] GetTileValues(int scenario)
    {
        if (scenario == 0)
        {
            int[,] tileOrder = new int[,]
            { { 111, 211, 312, 211, 111 },
              { 111, 111, 413, 411, 111 },
              { 111, 111, 211, 312, 211 },
              { 111, 111, 414, 412, 111 },
              { 111, 211, 512, 211, 111 }};
            return tileOrder;
        }
        else if(scenario == 1)
        {
            int[,] tileOrder = new int[,]
             { { 111, 211, 312, 211, 111 },
               { 111, 414, 412, 111, 111 },
               { 211, 312, 211, 111, 111 },
               { 111, 413, 411, 111, 111 },
               { 111, 211, 512, 211, 111 }};
            return tileOrder;
        }
        else
        {
            int[,] tileOrder = new int[,]
            { { 111, 211, 312, 211, 111 },
              { 111, 111, 312, 111, 111 },
              { 111, 211, 312, 211, 111 },
              { 111, 111, 312, 111, 111 },
              { 111, 211, 512, 211, 111 }};
            return tileOrder;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetTilePos(GameObject go, out int x, out int y)
    {
        for (int i = 0; i < m_LevelWidth; i++)
        {
            for (int j = 0; j < m_TileArray[i].Length; i++)
            {
                if (m_TileArray[i][j].Equals(go))
                {
                    x = i;
                    y = j;
                    return;
                }
            }
        }
        x = -1;
        y = -1;
        return;
    }

    public bool AddTileTo(GameObject go, int x, int y)
    {
        CheckArraySize(ref m_TileArray[x], y+1);
        if (IsValid(x, y))
        {
            go.transform.parent = this.transform;
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
