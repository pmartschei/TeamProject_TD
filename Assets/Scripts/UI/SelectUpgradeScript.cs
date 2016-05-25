using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectUpgradeScript : MonoBehaviour
{
    private GameObject m_selectedTower;
    private GameObject m_selectedTile;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Upgrade()
    {
        string value = gameObject.transform.FindChild("UpgradeCostText").GetComponent<Text>().text;
        int amount = int.Parse(value);
        GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseMoney(amount);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Destroy()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(m_selectedTower);
        m_selectedTile.GetComponent<OpenTowerHUDScript>().setTowerActive(false);
    }

    public void setSelectedTower(GameObject tower)
    {
        m_selectedTower = tower;
    }

    public void setSelectedTile(GameObject tile)
    {
        m_selectedTile = tile;
    }
}
