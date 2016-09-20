using UnityEngine;
using System.Collections;
using System;

public class EnemyScript : MonoBehaviour {

    public float m_MaxHp = 1.0f;
    public float m_Hp = 1.0f;
    public int m_DmgToBase = 1;
    public float m_Speed = 1.0f;
    public float m_WavePoints = 1.0f;
    [Range(0.01f,1.0f)]
    public float m_EnemyHealthDifficulty = 0.07f;
    [Range(0.01f, 1.0f)]
    public float m_EnemySpeedDifficulty = 0.01f;
    public int m_Gold = 1;

    public LifeAndMoneyScript m_LifeAndMoney;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
    void Update () {
	
	}

    public void DoDamage(float dmg,bool dmgBase=false)
    {
        m_Hp -= dmg;//leben abziehen
        if (Dead())
        {
            Destroy(this.gameObject);
            if (dmgBase)
            {
                m_LifeAndMoney.decreaseLife(m_DmgToBase);
            }
            else
            {
                m_LifeAndMoney.increaseMoney(m_Gold);
            }
        }
    }
    public bool Dead()
    {
        return m_Hp <= 0.0f;
    }
    public void IncreaseDifficulty(int difficulty)
    {
        float healthMultiplier = 1.0f + difficulty * m_EnemyHealthDifficulty;
        float speedMultiplier = Mathf.Min(2.0f,1.0f + difficulty * m_EnemySpeedDifficulty);
        m_MaxHp *= healthMultiplier;
        m_Hp = m_MaxHp;
        m_Speed *= speedMultiplier;
    }

    public void SetLifeAndMoneySystem(LifeAndMoneyScript lifeAndMoney)
    {
        m_LifeAndMoney = lifeAndMoney;
    }
}
