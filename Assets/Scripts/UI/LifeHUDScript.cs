using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeHUDScript : MonoBehaviour
{
    public GameObject m_lifeAndMoneySystem;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Text>().text = "" + m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().m_lifeTotal;
    }
}
