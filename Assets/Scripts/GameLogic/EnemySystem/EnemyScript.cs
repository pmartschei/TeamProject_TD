using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float m_MaxHp;
    public float m_Hp;
    public float m_DmgToBase;
    public float m_Speed;

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
}
