using UnityEngine;
using System.Collections;

public class PathTileScript : MonoBehaviour
{
    public bool m_north;
    public bool m_south;
    public bool m_west;
    public bool m_east;

    private bool[] directions = new bool[4];

	// Use this for initialization
	void Start ()
    {
        directions[0] = m_north;
        directions[1] = m_east;
        directions[2] = m_south;
        directions[3] = m_west;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    bool GetDirection(int index)
    {
        return directions[index];
    }
}
