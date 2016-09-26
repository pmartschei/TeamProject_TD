﻿using UnityEngine;
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

    public int m_currentLvl = int.MaxValue;

    //to see which number was generated
    public int m_GeneratedRandomValue;

    private GameObject[][] m_TileArray;
    private GameObject[][] m_villageArray;

    //info for camera
    private float m_maxCamera;


    public GameObject m_info;
    //expand background
    private CreateFloorScript m_floor;

    //set barricade
    private BarricadeScript m_barricade;

    void Start()
    {
        m_TileArray = new GameObject[m_LevelWidth][];
        for (int i = 0; i < m_TileArray.Length; i++)
        {
            m_TileArray[i] = new GameObject[m_LevelWidth];
        }

        m_villageArray = new GameObject[2][];
        for (int i = 0; i < m_villageArray.Length; ++i)
        {
            m_villageArray[i] = new GameObject[3];
        }

        m_floor = m_info.GetComponent<CreateFloorScript>();
        m_barricade = m_info.GetComponent<BarricadeScript>();

        Init();
    }

    private void Init()
    {
        System.Random rand = new System.Random();
        double randValue = rand.NextDouble();
        int scenario = (int)(randValue * 99);

        m_GeneratedRandomValue = scenario;

        CreateVillage();

        CreateScenario(scenario/33);

        CreateLevel(10);
    }

    internal void LevelFinished(int y)
    {
        if (m_currentLvl <= y)
        {
            RemoveGeneratedValue();
            CreateLevel(m_currentLvl + UnityEngine.Random.Range(5, 10));
        }
    }

    private void RemoveGeneratedValue()
    {
        for (int x = 0; x < m_LevelWidth; x++)
        {
            for (int y = 0; y < m_TileArray[x].Length; y++)
            {
                GameObject go = m_TileArray[x][y];
                if (go != null)
                {
                    TileScript ts = go.GetComponent<TileScript>();
                    if (ts != null)
                    {
                        ts.m_IsGenerated = false;
                    }
                }
            }
        }
    }

    private void CreateLevel(int y)
    {
        m_currentLvl = y;
        int startX = UnityEngine.Random.Range(0, m_LevelWidth);
        int endX = UnityEngine.Random.Range(0, m_LevelWidth);
        int currentX = startX;
        for (int depth = 0; depth <= 1; depth++)
        {
            bool horizontally = false;
            for (int i = 0; i < m_LevelWidth; i++)
            {
                int correctI = i;
                if (startX > endX)
                {
                    correctI = m_LevelWidth - i - 1;
                }
                bool curve = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f;
                if (depth==1 && horizontally)
                {
                    curve = false;
                }else if (depth == 1 && currentX != endX)
                {
                    curve = true;
                }
                if (currentX == endX)
                {
                    if (!horizontally)
                    {
                        curve = false;
                    }else
                    {
                        curve = true;
                    }
                }
                GameObject t;
                t = GameObject.Instantiate(m_TileSystem.GetComponent<TileSystem>().m_BlankVar1);
                if (correctI == currentX)
                {
                    if (curve)
                    {
                        if (!horizontally)
                        {
                            if (startX > endX)
                            {
                                currentX--;
                            }
                            else
                            {
                                currentX++;
                            }
                        }
                        t = GameObject.Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetCurveVar1);
                        PathTileScript path = t.GetComponent<PathTileScript>();
                        if (startX <= endX)
                        {
                            t.transform.Rotate(Vector3.forward, 90);
                            if (path != null)
                            {
                                path.Rotate();
                            }
                        }else
                        {
                            t.transform.Rotate(Vector3.forward, 180);
                            if (path != null)
                            {
                                path.Rotate();
                                path.Rotate();
                            }
                        }
                        if (!horizontally)
                        {
                            t.transform.Rotate(Vector3.forward, 180);
                            if (path != null)
                            {
                                path.Rotate();
                                path.Rotate();
                            }
                        }
                        horizontally = !horizontally;
                    }
                    else
                    {
                        if (horizontally)
                        {
                            if (startX > endX)
                            {
                                currentX--;
                            }
                            else
                            {
                                currentX++;
                            }
                        }
                        t = GameObject.Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetStraightVar1);
                        if (!horizontally)
                        {
                            t.transform.Rotate(Vector3.forward, 90);
                            PathTileScript path = t.GetComponent<PathTileScript>();
                            if (path != null)
                            {
                                path.Rotate();
                            }
                        }
                    }
                }

                TileScript ts = t.GetComponent<TileScript>();
                if (ts != null)
                {
                    ts.m_IsGenerated = true;
                }
                int realY = y + depth;
                t.transform.position = new Vector3(correctI * m_SizeX + m_SizeX / 2.0f, 0.0f, realY * m_SizeY + m_SizeY / 2.0f);
                t.transform.parent = this.transform;

                if(t.transform.position.z > m_maxCamera)
                    m_maxCamera = t.transform.position.z;

                AddTileTo(t, correctI, realY);
            }
        }

        m_floor.expandBG();
        m_barricade.drawBarricade();

    }

    private void CreateVillage()
    {
        GameObject tile = null;

        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_StreetEndVar1);
        tile.transform.Rotate(Vector3.forward, 90);
        PathTileScript path = tile.GetComponent<PathTileScript>();
        if (path != null)
        {
            path.Rotate();
        }
        AddVillagePart(tile, 1, 0);

        //tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_VillagePart2);
        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_villageLocked);
        tile.transform.Rotate(Vector3.forward, 180);
        AddVillagePart(tile, 1, -1);

        //tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_VillagePart3);
        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_villageLocked);
        AddVillagePart(tile, 1, 1);

        //tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_VillagePart4);
        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_villageLocked);
        tile.transform.Rotate(Vector3.forward, 270);
        AddVillagePart(tile, 0, -1);

        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_VillagePart1);
        tile.transform.Rotate(Vector3.forward, 90);
        AddVillagePart(tile, 0, 0);

        //tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_VillagePart5);
        tile = Instantiate(m_TileSystem.GetComponent<TileSystem>().m_villageLocked);
        tile.transform.Rotate(Vector3.forward, 90);
        AddVillagePart(tile, 0, 1);
    }

    public void AddVillagePart(GameObject tile, int x, int y)
    {
        float xValue = x - 2;
        float yValue = y + m_LevelWidth/2;
        tile.transform.position = new Vector3(yValue * m_SizeX + m_SizeX / 2.0f, 0.0f, xValue * m_SizeY + m_SizeY / 2.0f);
        tile.transform.parent = this.transform;
        m_villageArray[x][y + 1] = tile;
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
                    //tile.transform.Rotate(0.0f, 90.0f, 0.0f);
                    path = tile.GetComponent<PathTileScript>();
                    if(path != null)
                        path.Rotate();
                }
                else if (tileRotationValue == 3)
                {
                    tile.transform.Rotate(Vector3.forward, 180);
                    //tile.transform.Rotate(0.0f, 180.0f, 0.0f);
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
                    //tile.transform.Rotate(0.0f, 270.0f, 0.0f);
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
              { 121, 111, 423, 421, 111 },
              { 111, 121, 211, 322, 221 },
              { 121, 111, 414, 412, 121 },
              { 111, 221, 312, 211, 111 }};
            return tileOrder;
        }
        else if(scenario == 1)
        {
            int[,] tileOrder = new int[,]
             { { 111, 211, 312, 211, 121 },
               { 111, 424, 422, 111, 111 },
               { 211, 312, 211, 111, 121 },
               { 111, 413, 411, 111, 121 },
               { 111, 211, 312, 211, 121 }};
            return tileOrder;
        }
        else
        {
            int[,] tileOrder = new int[,]
            { { 111, 211, 312, 221, 111 },
              { 111, 111, 312, 121, 111 },
              { 111, 211, 322, 221, 121 },
              { 121, 111, 312, 111, 111 },
              { 121, 211, 322, 221, 121 }};
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
            for (int j = 0; j < m_TileArray[i].Length; j++)
            {
                if (m_TileArray[i][j]!=null && m_TileArray[i][j].Equals(go))
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
        Collider[] colliders = go.GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }
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
                GameObject go = m_TileArray[x][y];
                if (go == null) return false;

                TileScript ts = go.GetComponent<TileScript>();

                if (ts != null)
                {
                    return !ts.m_IsGenerated;
                }

                return true;
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

    public float GetCameraMax()
    {
        return m_maxCamera;
    }
}
