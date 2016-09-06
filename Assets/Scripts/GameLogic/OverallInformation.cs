using UnityEngine;
using System.Collections;

public class OverallInformation : MonoBehaviour {

    private int m_numberOfLumberjacks;

	// Use this for initialization
	void Start () 
    {
        m_numberOfLumberjacks = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public int GetNumberOfLumberjacks()
    {
        return m_numberOfLumberjacks;
    }

    public void IncrementLumberjacks()
    {
        m_numberOfLumberjacks++;
    }

    public void DecrementLumberjacks()
    {
        m_numberOfLumberjacks--;
    }

    public bool AddLumberjackPossible()
    {
        if (m_numberOfLumberjacks == 0)
            return true;
        return false;
    }
}
