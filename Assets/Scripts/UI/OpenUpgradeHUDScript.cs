using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.GameLogic.TowerSystem;

public class OpenUpgradeHUDScript : MonoBehaviour
{
    private GameObject m_lifeAndMoneySystem;
    private GameObject m_upgradeTowerHUD;
    private int m_upgradeCost;

    private Transform m_target;
    private Camera m_camera;

    // Use this for initialization
    void Start()
    {
        m_upgradeTowerHUD = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_upgradeCost = int.Parse(m_upgradeTowerHUD.transform.FindChild("Upgrade").transform.FindChild("UpgradeCostText").GetComponent<Text>().text);

        m_target = gameObject.transform;
        m_camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
                m_upgradeTowerHUD.SetActive(false);


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_upgradeCost)))
            {
                m_upgradeTowerHUD.transform.FindChild("Upgrade").transform.FindChild("UpgradeButton").GetComponent<Button>().interactable = false;
                m_upgradeTowerHUD.transform.FindChild("Upgrade").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_upgradeTowerHUD.transform.FindChild("Upgrade").transform.FindChild("UpgradeButton").GetComponent<Button>().interactable = true;
                m_upgradeTowerHUD.transform.FindChild("Upgrade").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        else
        {
            m_upgradeTowerHUD.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            if (!GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject.activeSelf)
            {
                Vector3 screenPos = m_camera.WorldToScreenPoint(m_target.position);
                screenPos += new Vector3(0.0f, 10.0f, 0.0f);
                m_upgradeTowerHUD.transform.position = screenPos;
                m_upgradeTowerHUD.SetActive(true);
                m_upgradeTowerHUD.transform.FindChild("Destroy").GetComponent<SelectUpgradeScript>().setSelectedTower(gameObject);
            }
        }
    }
}
