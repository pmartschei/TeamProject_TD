using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnemyHealthScript : MonoBehaviour
{
    private GameObject m_healthBar;

    private int m_maxHealth;
    private Vector3 m_offset;

	// Use this for initialization
	void Start ()
    {
        m_healthBar = GameObject.Find("UISystem").GetComponent<UISystemScript>().m_HealthBarSlider;
        m_healthBar.GetComponent<Slider>().maxValue = gameObject.GetComponent<EnemyScript>().m_MaxHp;
        m_healthBar.GetComponent<Slider>().minValue = 0;

        m_offset = new Vector3(0.0f, 0.25f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_healthBar.transform.position = gameObject.transform.position + m_offset;
        m_healthBar.GetComponent<Slider>().value = gameObject.GetComponent<EnemyScript>().m_Hp;
	}
}
