using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.GameLogic.TowerSystem;

public class SelectUpgradeScript : MonoBehaviour
{
    private GameObject m_selectedTower;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Upgrade(GameObject upgrade)
    {
        if (Time.timeScale != 0)
        {
            int index = int.Parse(upgrade.name.Substring(upgrade.name.Length - 1));
            string value = upgrade.transform.FindChild("UpgradeCostText").GetComponent<Text>().text;
            int amount = int.Parse(value);
            GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseMoney(amount);
            this.gameObject.SetActive(false);
            TowerScript ts= m_selectedTower.GetComponent<TowerScript>();
            if (ts != null)
            {
                ts.AddUpgrade(index);
            }
            OpenUpgradeHUDScript openUpgrade = m_selectedTower.GetComponent<OpenUpgradeHUDScript>();
            if (openUpgrade != null)
            {
                openUpgrade.unshowProps();
            }
        }
    }

    public void Destroy()
    {
        if (Time.timeScale != 0)
        {
            this.gameObject.SetActive(false);
            Destroy(m_selectedTower);
        }
    }

    public void setSelectedTower(GameObject tower)
    {
        m_selectedTower = tower;
    }
}
