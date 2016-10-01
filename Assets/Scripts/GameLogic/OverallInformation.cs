using UnityEngine;
using System.Collections;

public class OverallInformation : MonoBehaviour {

    public Camera m_curretCam;
    private int m_numberOfLumberjacks;
    private bool m_deletable;

    public bool m_FireTotemActive = false;
    public bool m_WaterTotemActive = false;
    public bool m_AirTotemActive = false;
    public bool m_EarthTotemActive = false;
    public float m_FireTotemMultiplier = 1.2f;
    public float m_EarthTotemMultiplier = 0.8f;
    public float m_AirTotemMultiplier = 0.25f;
    public float m_WaterTotemTimer = 60f;
    private float m_WaterCooldown = 0.0f;

    // Use this for initialization
    void Start () 
    {
        m_numberOfLumberjacks = 0;
        m_deletable = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_WaterTotemActive) return;
	 
        if (m_WaterCooldown <= 0.0f)
        {
            LifeAndMoneyScript s = GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>();
            s.increaseLife(1);
            m_WaterCooldown = m_WaterTotemTimer;
        }else
        {
            m_WaterCooldown -= Time.deltaTime;
        }
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

    public bool GetDeletable()
    {
        return m_deletable;
    }

    public void SetDeletable(bool deletable)
    {
        m_deletable = deletable;
    }
}
