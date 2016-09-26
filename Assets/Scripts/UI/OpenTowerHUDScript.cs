using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpenTowerHUDScript : MonoBehaviour
{
    private GameObject m_lifeAndMoneySystem;

    private GameObject m_buildTowerHUD;
    private GameObject m_totemHUD;
    private GameObject m_upgradeTower;

    private int m_firstTowerCost;
    private int m_secondTowerCost;
    private int m_thirdTowerCost;

    private GameObject m_upperLeftTower;
    private GameObject m_upperRightTower;
    private GameObject m_lowerTower;

    private GameObject m_upperLeftTowerCircle;
    private GameObject m_upperRightTowerCircle;
    private GameObject m_lowerTowerCircle;

    private GameObject m_upperLeftTowerButton;
    private GameObject m_upperRightTowerButton;
    private GameObject m_lowerTowerButton;

    private Transform m_target;
    private Camera m_camera;

    public bool m_towerBuilt = false;

    // Use this for initialization
    // Get the HUD and MoneySystem
    // Get the amount for each tower
    // Get the main Camera and the position of the tile for the correct view
    void Start()
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_totemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;

        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_upperLeftTower = m_buildTowerHUD.transform.FindChild("UpperLeftTower").gameObject;
        m_upperRightTower = m_buildTowerHUD.transform.FindChild("UpperRightTower").gameObject;
        m_lowerTower = m_buildTowerHUD.transform.FindChild("LowerTower").gameObject;

        m_firstTowerCost = int.Parse(m_upperLeftTower.transform.FindChild("TowerCostText").GetComponent<Text>().text);
        m_secondTowerCost = int.Parse(m_upperRightTower.transform.FindChild("TowerCostText").GetComponent<Text>().text);
        m_thirdTowerCost = int.Parse(m_lowerTower.transform.FindChild("TowerCostText").GetComponent<Text>().text);

        m_upperLeftTowerButton = m_upperLeftTower.transform.FindChild("TowerButton").gameObject;
        m_upperRightTowerButton = m_upperRightTower.transform.FindChild("TowerButton").gameObject;
        m_lowerTowerButton = m_lowerTower.transform.FindChild("TowerButton").gameObject;

        m_upperLeftTowerCircle = m_upperLeftTower.transform.FindChild("CircleImage").gameObject;
        m_upperRightTowerCircle = m_upperRightTower.transform.FindChild("CircleImage").gameObject;
        m_lowerTowerCircle = m_lowerTower.transform.FindChild("CircleImage").gameObject;

        m_target = gameObject.transform;
        m_camera = Camera.main;
    }

    // Update is called once per frame
    // Turn off HUD if the right mouse button is pressed
    // Check for every tower option if enough money is available to build it, if not decrease alpha values and disable the button
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Escape))
                m_buildTowerHUD.SetActive(false);

            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_firstTowerCost)))
            {
                m_upperLeftTowerButton.GetComponent<Button>().interactable = false;
                m_upperLeftTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_upperLeftTowerButton.GetComponent<Button>().interactable = true;
                m_upperLeftTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_secondTowerCost)))
            {
                m_upperRightTowerButton.GetComponent<Button>().interactable = false;
                m_upperRightTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_upperRightTowerButton.GetComponent<Button>().interactable = true;
                m_upperRightTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_thirdTowerCost)))
            {
                m_lowerTowerButton.GetComponent<Button>().interactable = false;
                m_lowerTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_lowerTowerButton.GetComponent<Button>().interactable = true;
                m_lowerTowerCircle.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }

            if(m_buildTowerHUD.activeSelf)
            {

            }
        }
    }

    // position the hud at the right place
    // transfer the position of the current tile to SelectTowerScript
    public void OnMouseDown()
    {
        if (Time.timeScale != 0 && !m_towerBuilt)
        {
            m_upgradeTower.SetActive(false);
            m_totemHUD.SetActive(false);

            m_buildTowerHUD.transform.position = m_target.position;
            m_buildTowerHUD.transform.position += new Vector3(0.0f, 0.5f, 0.0f);

            m_buildTowerHUD.SetActive(true);

            // GameObject.Find("HUDCanvas").transform.FindChild("UpgradeTowerHUD").FindChild("Destroy").GetComponent<SelectUpgradeScript>().setSelectedTile(gameObject);

            Vector3 screenPos = m_camera.WorldToScreenPoint(m_target.position);
            screenPos += new Vector3(0.0f, 10.0f, 0.0f);
            m_buildTowerHUD.transform.position = screenPos;

            m_upperLeftTower.GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
            m_upperRightTower.GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
            m_lowerTower.GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);      
        }
    }
}
