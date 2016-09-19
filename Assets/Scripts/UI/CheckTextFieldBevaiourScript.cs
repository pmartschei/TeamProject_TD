using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckTextFieldBevaiourScript : MonoBehaviour
{
    public GameObject m_undoButtonHUD;
    public GameObject m_drawTilesSystem;
    public GameObject m_lifeAndMoneySystem;

    private int m_cost;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
        m_cost = m_undoButtonHUD.GetComponent<UndoButttonBehaviourScript>().m_undoCost;
        gameObject.transform.FindChild("Text").transform.GetComponentInChildren<Text>().text = "Pay " + m_cost + " Gold to reset all reserved Tiles?";
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Yes()
    {
        m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().decreaseMoney(m_cost);
        m_drawTilesSystem.GetComponent<TileUI>().resetTiles();
        gameObject.SetActive(false);
    }
}
