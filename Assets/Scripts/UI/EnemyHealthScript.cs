using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnemyHealthScript : MonoBehaviour
{
    private GameObject m_healthBar;
    public Image m_Fill;

    private int m_maxHealth;
    private Vector3 m_offset;

	// Use this for initialization
	void Start ()
    {
        m_offset = new Vector3(0.0f, 0.25f, 0.0f);
        m_healthBar = (GameObject)Instantiate(GameObject.Find("UISystem").GetComponent<UISystemScript>().m_HealthBarSlider,gameObject.transform.position+m_offset,Quaternion.identity);
        m_healthBar.transform.SetParent(GameObject.Find("WorldSpaceHUDCanvas").transform);
        m_healthBar.transform.localScale = Vector3.one;
        m_healthBar.transform.localRotation = Quaternion.Euler(Vector3.zero);
        m_healthBar.GetComponent<Slider>().maxValue = gameObject.GetComponent<EnemyScript>().m_MaxHp;
        m_healthBar.GetComponent<Slider>().minValue = 0;
        m_Fill = m_healthBar.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
    }

    void OnDestroy()
    {
        Destroy(m_healthBar);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_healthBar.transform.position = gameObject.transform.position + m_offset;
        m_healthBar.GetComponent<Slider>().value = gameObject.GetComponent<EnemyScript>().m_Hp;
        float percent = (1-gameObject.GetComponent<EnemyScript>().m_Hp / gameObject.GetComponent<EnemyScript>().m_MaxHp);
        if (m_Fill!=null)
            m_Fill.color = new Color(percent, (1 - percent), 0);
	}
}
