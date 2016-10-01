using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTotemScript : MonoBehaviour
{
    public GameObject m_fireTotem;
    public GameObject m_earthTotem;
    public GameObject m_waterTotem;
    public GameObject m_windTotem;

    public GameObject m_tileSystem;

    public GameObject m_totemPlace;

    public GameObject m_fieldObj;
    private FieldScript m_field;

    // Use this for initialization
    void Start ()
    {
        m_field = m_fieldObj.GetComponent<FieldScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void buildTotem()
    {
        if (Time.timeScale != 0)
        {
            string value = gameObject.transform.FindChild("TotemCostText").GetComponent<Text>().text;
            int amount = int.Parse(value);
            GameObject.Find("LifeAndMoneySystem").GetComponent<LifeAndMoneyScript>().decreaseWood(amount);
            gameObject.transform.parent.gameObject.SetActive(false);

            Vector3 pos = m_totemPlace.transform.position;
            Destroy(m_totemPlace);
            GameObject tile = null;
            string name = gameObject.name;
            switch (name)
            {
                case "FireTotem":
                   
                    tile = Instantiate(m_tileSystem.GetComponent<TileSystem>().m_VillagePart4);
                    m_fireTotem.SetActive(false);
                    GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_FireTotemActive = true;
                    GameObject.Find("TowerSystem").GetComponent<TowerSystemScript>().RecalculateAttributes();
                    break;

                case "EarthTotem":
                    tile = Instantiate(m_tileSystem.GetComponent<TileSystem>().m_VillagePart5);
                    m_earthTotem.SetActive(false);
                    GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_EarthTotemActive = true;
                    break;

                case "WaterTotem":
                    tile = Instantiate(m_tileSystem.GetComponent<TileSystem>().m_VillagePart3);
                    m_waterTotem.SetActive(false);
                    GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_WaterTotemActive = true;
                    break;

                case "WindTotem":
                    tile = Instantiate(m_tileSystem.GetComponent<TileSystem>().m_VillagePart2);
                    m_windTotem.SetActive(false);
                    GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_AirTotemActive = true;
                    break;

                default: break;
            }

            tile.transform.position = pos;
            tile.transform.Rotate(Vector3.forward, 180);
            tile.transform.parent = m_field.transform;
        }
    }
}
