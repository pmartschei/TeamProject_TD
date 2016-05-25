using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTowerScript : MonoBehaviour
{
    private Vector3 m_position;
    private GameObject m_selectedTile;
    private bool m_towerBuild = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Get the amount of money needed for the tower and decrease that amount from the MoneySystem
    // turn off the build hud and build the corrosponding tower
    public void buildTower()
    {
        string value = gameObject.transform.FindChild("TowerCostText").GetComponent<Text>().text;
        int amount = int.Parse(value);
        GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseMoney(amount);
        gameObject.transform.parent.gameObject.SetActive(false);

        string name = gameObject.name;
        switch (name)
        {
            case "UpperLeftTower":
                GameObject tower1 = Instantiate(GameObject.Find("TowerSystem").GetComponent<TowerSystemScript>().m_tower1);
                tower1.transform.position = m_position;
                tower1.transform.parent = GameObject.Find("TowerSystem").gameObject.transform;
                break;

            case "UpperRightTower":
                GameObject tower2 = Instantiate(GameObject.Find("TowerSystem").GetComponent<TowerSystemScript>().m_tower2);
                tower2.transform.position = m_position;
                tower2.transform.parent = GameObject.Find("TowerSystem").gameObject.transform;
                break;

            case "LowerRightTower":
                GameObject tower3 = Instantiate(GameObject.Find("TowerSystem").GetComponent<TowerSystemScript>().m_tower3);
                tower3.transform.position = m_position;
                tower3.transform.parent = GameObject.Find("TowerSystem").gameObject.transform;
                break;

            case "LowerLeftTower":
                GameObject tower4 = Instantiate(GameObject.Find("TowerSystem").GetComponent<TowerSystemScript>().m_tower4);
                tower4.transform.position = m_position;
                tower4.transform.parent = GameObject.Find("TowerSystem").gameObject.transform;
                break;

            default: break;
        }

        m_towerBuild = true;
        m_selectedTile.GetComponent<OpenTowerHUDScript>().setTowerActive(m_towerBuild);
    }

    // Gets the position of the selected Buildtile
    // Move the Object on the y-axis to align it with the surface of the tile
    public void setPosition(Vector3 position, GameObject tile)
    {
        m_selectedTile = tile;
        m_position = position;
        m_position += new Vector3(0.0f, 0.7f, 0.0f);
    }
}
