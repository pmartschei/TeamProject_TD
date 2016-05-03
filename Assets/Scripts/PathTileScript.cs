using UnityEngine;
using System.Collections;

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
    public void Rotate()
    {
        bool northCopy = m_north;
        m_north = m_west;
        m_west = m_south;
        m_south = m_east;
        m_east = northCopy;
    }
}
