using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTotemScript : MonoBehaviour
{
    public GameObject m_fireTotem;
    public GameObject m_earthTotem;
    public GameObject m_waterTotem;
    public GameObject m_windTotem;

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

            GameObject tile = null;

            string name = gameObject.name;
            switch (name)
            {
                case "FireTotem":
                    //Debug.Log("Michi bau hier ein was passieren soll!");
                    //tile = Instantiate(m_fireTotem);
                    //tile.transform.Rotate(Vector3.forward, 180);
                    //m_field.AddVillagePart(tile, 1, -1);
                    m_fireTotem.SetActive(false);
                    break;

                case "EarthTotem":
                    Debug.Log("Michi bau hier ein was passieren soll!");
                    m_earthTotem.SetActive(false);
                    break;

                case "WaterTotem":
                    Debug.Log("Michi bau hier ein was passieren soll!");
                    m_waterTotem.SetActive(false);
                    break;

                case "WindTotem":
                    Debug.Log("Michi bau hier ein was passieren soll!");
                    m_windTotem.SetActive(false);
                    break;

                default: break;
            }
        }
    }
}
