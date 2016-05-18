using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathTileScript : MonoBehaviour
{

    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;
    public bool m_north;
    public bool m_south;
    public bool m_west;
    public bool m_east;

    public List<GameObject> m_PathNorth = new List<GameObject>();
    public List<GameObject> m_PathEast = new List<GameObject>();
    public List<GameObject> m_PathSouth = new List<GameObject>();
    public List<GameObject> m_PathWest = new List<GameObject>();

    public List<GameObject> m_ParentObject=new List<GameObject>();
    public List<int> m_ParentDirection = new List<int>();

    private bool[] m_directions;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetDirection(int index)
    {
        switch (index)
        {
            case NORTH: return m_north;
            case SOUTH: return m_south;
            case EAST: return m_east;
            case WEST: return m_west;
        }
        return false;
    }
    public List<GameObject> GetPath(int index)
    {
        switch (index)
        {
            case NORTH: return m_PathNorth;
            case SOUTH: return m_PathSouth;
            case EAST: return m_PathEast;
            case WEST: return m_PathWest;
        }
        return null;
    }
    public void Rotate()
    {
        bool northCopy = m_north;
        m_north = m_west;
        m_west = m_south;
        m_south = m_east;
        m_east = northCopy;
        List<GameObject> pathCopy = m_PathNorth;
        m_PathNorth = m_PathWest;
        m_PathWest = m_PathSouth;
        m_PathSouth = m_PathEast;
        m_PathEast = pathCopy;
    }
}
