using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenTotemsScript : MonoBehaviour
{
    private GameObject m_buildTowerHUD;
    private GameObject m_upgradeTower;

    private GameObject m_lifeAndMoneySystem;

    private GameObject m_TotemHUD;
    private int m_fireTotemCost;
    private int m_earthTotemCost;
    private int m_waterTotemCost;
    private int m_windTotemCost;

    private GameObject m_fireTotem;
    private GameObject m_earthTotem;
    private GameObject m_waterTotem;
    private GameObject m_windTotem;

    private GameObject m_fireTotemCircle;
    private GameObject m_earthTotemCircle;
    private GameObject m_waterTotemCircle;
    private GameObject m_windTotemCircle;

    private GameObject m_fireTotemButton;
    private GameObject m_earthTotemButton;
    private GameObject m_waterTotemButton;
    private GameObject m_windTotemButton;

    private Transform m_target;
    private Camera m_camera;

    // Use this for initialization
    void Start ()
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;

        m_TotemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_fireTotem = m_TotemHUD.transform.FindChild("FireTotem").gameObject;
        m_earthTotem = m_TotemHUD.transform.FindChild("EarthTotem").gameObject;
        m_waterTotem = m_TotemHUD.transform.FindChild("WaterTotem").gameObject;
        m_windTotem = m_TotemHUD.transform.FindChild("WindTotem").gameObject;

        m_fireTotemCost = int.Parse(m_fireTotem.transform.FindChild("TotemCostText").GetComponent<Text>().text);
        m_earthTotemCost = int.Parse(m_earthTotem.transform.FindChild("TotemCostText").GetComponent<Text>().text);
        m_waterTotemCost = int.Parse(m_waterTotem.transform.FindChild("TotemCostText").GetComponent<Text>().text);
        m_windTotemCost = int.Parse(m_windTotem.transform.FindChild("TotemCostText").GetComponent<Text>().text);

        m_fireTotemButton = m_fireTotem.transform.FindChild("TotemButton").gameObject;
        m_earthTotemButton = m_earthTotem.transform.FindChild("TotemButton").gameObject;
        m_waterTotemButton = m_waterTotem.transform.FindChild("TotemButton").gameObject;
        m_windTotemButton = m_windTotem.transform.FindChild("TotemButton").gameObject;

        m_fireTotemCircle = m_fireTotem.transform.FindChild("CircleImage").gameObject;
        m_earthTotemCircle = m_earthTotem.transform.FindChild("CircleImage").gameObject;
        m_waterTotemCircle = m_waterTotem.transform.FindChild("CircleImage").gameObject;
        m_windTotemCircle = m_windTotem.transform.FindChild("CircleImage").gameObject;

        m_target = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Escape))
                m_TotemHUD.SetActive(false);

            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_fireTotemCost)))
            {
                m_fireTotemButton.GetComponent<Button>().interactable = false;
                m_fireTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_fireTotemButton.GetComponent<Button>().interactable = true;
                m_fireTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_earthTotemCost)))
            {
                m_earthTotemButton.GetComponent<Button>().interactable = false;
                m_earthTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_earthTotemButton.GetComponent<Button>().interactable = true;
                m_earthTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_waterTotemCost)))
            {
                m_waterTotemButton.GetComponent<Button>().interactable = false;
                m_waterTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_waterTotemButton.GetComponent<Button>().interactable = true;
                m_waterTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }

            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_windTotemCost)))
            {
                m_windTotemButton.GetComponent<Button>().interactable = false;
                m_windTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_windTotemButton.GetComponent<Button>().interactable = true;
                m_windTotemCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
    }

    public void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            if (GameObject.Find("MouseSystem").GetComponent<MouseScript>().m_MouseState == MouseScript.MouseState.BuildTile)
            {
                GameObject.Find("MouseSystem").GetComponent<MouseScript>().Deactivate();
            }
            else if (GameObject.Find("MouseSystem").GetComponent<MouseScript>().m_MouseState == MouseScript.MouseState.Bolt) { return; }
            m_upgradeTower.GetComponent<SelectUpgradeScript>().Unshow();
            m_upgradeTower.SetActive(false);
            m_buildTowerHUD.SetActive(false);

            m_TotemHUD.transform.position = m_target.position;
            m_TotemHUD.transform.position += new Vector3(0.0f, 0.5f, 0.0f);

            m_TotemHUD.SetActive(true);

            Camera cam = GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_curretCam;
            Vector3 screenPos =cam.WorldToScreenPoint(m_target.position);
            screenPos += new Vector3(0.0f, 40.0f, 0.0f);
            m_TotemHUD.transform.position = screenPos;
            SelectTotemScript[] scripts = m_TotemHUD.GetComponentsInChildren<SelectTotemScript>();
            foreach (SelectTotemScript script in scripts)
            {
                script.m_totemPlace = this.gameObject;
            }

        }   
    }
}
