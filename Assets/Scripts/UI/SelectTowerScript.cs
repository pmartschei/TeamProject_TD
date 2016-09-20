using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.GameLogic.TowerSystem;

public class SelectTowerScript : MonoBehaviour
{
    private Vector3 m_position;
    private GameObject m_selectedTile;

    private GameObject m_towerSystem;

    // Use this for initialization
    void Start()
    {
        m_towerSystem = GameObject.Find("TowerSystem").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Get the amount of money needed for the tower and decrease that amount from the MoneySystem
    // turn off the build hud and build the corrosponding tower
    public void buildTower()
    {
        if (Time.timeScale != 0)
        {
            string value = gameObject.transform.FindChild("TowerCostText").GetComponent<Text>().text;
            int amount = int.Parse(value);
            GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseMoney(amount);
            gameObject.transform.parent.gameObject.SetActive(false);

            string name = gameObject.name;
            GameObject tower = null;
            switch (name)
            {
                case "UpperLeftTower":
                    tower = Instantiate(m_towerSystem.GetComponent<TowerSystemScript>().m_tower1);
                    break;

                case "UpperRightTower":
                    tower = Instantiate(m_towerSystem.GetComponent<TowerSystemScript>().m_tower2);
                    break;

                case "LowerTower":
                    tower = Instantiate(m_towerSystem.GetComponent<TowerSystemScript>().m_tower3);
                    break;

                default: break;
            }
            if (null != tower)
            {

                tower.transform.position = m_position;
                tower.transform.parent = m_towerSystem.gameObject.transform;
                tower.GetComponent<TowerScript>().m_field = m_selectedTile;
                OpenTowerHUDScript feld = m_selectedTile.GetComponent<OpenTowerHUDScript>();
                if (feld != null)
                {
                    feld.m_towerBuilt = true;
                    Debug.Log("True");
                }
            }
        }
    }

    // Gets the position of the selected Buildtile
    // Move the Object on the y-axis to align it with the surface of the tile
    public void setPosition(Vector3 position, GameObject tile)
    {
        // m_selectedTile = tile;
        // pos stimmt nur für tower 1 (mage tower)
        m_position = position;
        m_position += new Vector3(0.0f, 0.25f, 0.0f);
        m_selectedTile = tile;
    }
}
