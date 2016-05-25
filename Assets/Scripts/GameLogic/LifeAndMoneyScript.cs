﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeAndMoneyScript : MonoBehaviour
{
    public int m_lifeTotal;
    public int m_MoneyTotal;
    public GameObject m_HeartText;
    public GameObject m_MoneyText;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void decreaseLife(int amount)
    {
        if(m_lifeTotal - amount >= 0)
        {
            m_lifeTotal -= amount;
        }
    }

    public void decreaseMoney(int amount)
    {
        if (m_MoneyTotal - amount >= 0)
        {
            m_MoneyTotal -= amount;
        }
    }

    public void increaseLife(int amount)
    {
        m_lifeTotal += amount;
    }

    public void increaseMoney(int amount)
    {
        m_MoneyTotal += amount;
    }

    public bool isMoneyDecreasePossible(int amount)
    {
        if (m_MoneyTotal - amount >= 0)
        {
            return true;
        }

        return false;
    }
}
