using UnityEngine;
using System.Collections;

public class FieldScript : MonoBehaviour
{

    // Use this for initialization
    public int m_LevelWidth;
    public GameObject m_SimpleInitTile;
    public GameObject m_SimpleInitStreetTile;
    private GameObject[][] m_TileArray;
    void Start()
    {
        m_TileArray = new GameObject[m_LevelWidth][];
        for (int i = 0; i < m_TileArray.Length; i++)
        {
            m_TileArray[i] = new GameObject[m_LevelWidth];
        }
        SimpleInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SimpleInit()
    {
        for (int x = 0; x < m_LevelWidth; x++)
        {
            for (int y = 0; y < m_LevelWidth; y++)
            {
                if (x == 3 && y == 4)
                {
                    GameObject tile = Instantiate(m_SimpleInitStreetTile);
                    Mesh mesh = tile.GetComponent<MeshFilter>().mesh;
                    Vector3 size = mesh.bounds.size;
                    tile.transform.position = new Vector3(x * size.x + size.x / 2.0f, -size.z / 2.0f, y * size.y + size.y / 2.0f);
                    AddTileTo(tile, x, y);
                }
                else
                {
                    GameObject tile = Instantiate(m_SimpleInitTile);
                    Mesh mesh = tile.GetComponent<MeshFilter>().mesh;
                    Vector3 size = mesh.bounds.size;
                    tile.transform.position = new Vector3(x * size.x + size.x / 2.0f, -size.z / 2.0f, y * size.y + size.y / 2.0f);
                    AddTileTo(tile, x, y);
                }
            }
        }
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
