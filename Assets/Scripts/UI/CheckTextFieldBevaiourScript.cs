using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckTextFieldBevaiourScript : MonoBehaviour
{
    public GameObject m_undoButtonHUD;
    public GameObject m_drawTilesSystem;
    public GameObject m_lifeAndMoneySystem;

    private GameObject m_buildTowerHUD;
    private GameObject m_totemHUD;
    private GameObject m_upgradeTower;

    private int m_cost;

	// Use this for initialization
	void Start ()
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_totemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;

        gameObject.SetActive(false);
        m_cost = m_undoButtonHUD.GetComponent<UndoButttonBehaviourScript>().m_undoCost;
        gameObject.transform.FindChild("Text").transform.GetComponentInChildren<Text>().text = "Pay " + m_cost + " Gold to reset all reserved Tiles?";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
	}

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        m_upgradeTower.SetActive(false);
        m_totemHUD.SetActive(false);
        m_buildTowerHUD.SetActive(false);

        gameObject.SetActive(true);
    }

    public void Yes()
    {
        m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().decreaseMoney(m_cost);
        m_drawTilesSystem.GetComponent<TileUI>().resetTiles();
        gameObject.SetActive(false);
    }
}
