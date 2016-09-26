using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeAndMoneyScript : MonoBehaviour
{
    public int m_lifeTotal;
    public int m_MoneyTotal;
    public int m_woodTotal;
    public GameObject m_HeartText;
    public GameObject m_MoneyText;
    public GameObject m_WoodText;

    private GameObject m_GameOver;

	// Use this for initialization
	void Start ()
    {
        m_GameOver = GameObject.Find("HUDCanvas").transform.FindChild("Panel").transform.FindChild("GameOverMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void decreaseLife(int amount)
    {
            m_lifeTotal -= amount;
        if (m_lifeTotal < 0) m_lifeTotal = 0;
        if (m_lifeTotal == 0)
            m_GameOver.GetComponent<GameOverScript>().activate();
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

    public void decreaseWood(int amount)
    {
        if (m_woodTotal - amount >= 0)
            m_woodTotal -= amount;
    }

    public void increaseWood(int amount)
    {
        m_woodTotal += amount;
    }

    public bool isWoodDecreasePossible(int amount)
    {
        if (m_woodTotal - amount >= 0)
            return true;

        return false;
    }
}
