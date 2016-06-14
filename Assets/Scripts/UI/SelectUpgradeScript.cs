using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public void Upgrade()
    {
        if (Time.timeScale != 0)
        {
            string value = gameObject.transform.FindChild("UpgradeCostText").GetComponent<Text>().text;
            int amount = int.Parse(value);
            GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseMoney(amount);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void Destroy()
    {
        if (Time.timeScale != 0)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            Destroy(m_selectedTower);
        }
    }

    public void setSelectedTower(GameObject tower)
    {
        m_selectedTower = tower;
    }
}
