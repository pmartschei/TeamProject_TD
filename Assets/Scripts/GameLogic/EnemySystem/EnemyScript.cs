using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float m_MaxHp;
    public float m_Hp;
    public float m_DmgToBase;
    public float m_Speed;
    public float m_WavePoints;
    [Range(0.01f,1.0f)]
    public float m_EnemyHealthDifficulty = 0.07f;
    [Range(0.01f, 1.0f)]
    public float m_EnemySpeedDifficulty = 0.01f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
    void Update () {
	
	}

    public void DoDamage(float dmg)
    {
        m_Hp -= dmg;//leben abziehen
        if (m_Hp <= 0.0f)
        {
            Destroy(this.gameObject);
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
}
