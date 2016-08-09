using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.GameLogic.TowerSystem;



public class OpenUpgradeHUDScript : MonoBehaviour
{

    private static int MAX_UPGRADES = 3;
    private static float UPGRADE_DISTANCE = 100;
    private static float START_RADIANS = 90 * Mathf.Deg2Rad;

    private GameObject m_lifeAndMoneySystem;
    private GameObject m_upgradeTowerHUD;

    private GameObject m_selectedTile;

    private Transform m_target;

    // Use this for initialization
    void Start()
    {
        m_upgradeTowerHUD = GameObject.Find("WorldSpaceHUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_target = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
                m_upgradeTowerHUD.SetActive(false);
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
            if (!GameObject.Find("WorldSpaceHUDCanvas").transform.FindChild("BuildTowerHUD").gameObject.activeSelf)
            {
                m_upgradeTowerHUD.transform.position = m_target.position;
                m_upgradeTowerHUD.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                TowerScript tw = GetComponent<TowerScript>();
                if (tw == null) return;
                Upgrade[] upgrades = tw.GetNextUpgrades();

                showUpgrades(upgrades);
            }
        }
    }

    private void showUpgrades(Upgrade[] upgrades)
    {
        m_upgradeTowerHUD.SetActive(true);
        int count = Mathf.Min(MAX_UPGRADES, upgrades.Length);

        float radians = (-360 / (count + 1)) * Mathf.Deg2Rad;

        int i = 0;
        for (; i < count; i++)
        {
            string name = "Upgrade" + i;

            GameObject upgradeHUD = m_upgradeTowerHUD.transform.FindChild(name).gameObject;

            RectTransform transform = upgradeHUD.GetComponent<RectTransform>();

            float x = Mathf.Cos(radians * (i + 1) -START_RADIANS);
            float y = Mathf.Sin(radians * (i + 1) - START_RADIANS);

            transform.anchoredPosition = new Vector2(x*UPGRADE_DISTANCE, y*UPGRADE_DISTANCE);

            upgradeHUD.SetActive(true);

            upgradeHUD.transform.FindChild("UpgradeCostText").GetComponent<Text>().text = "" + upgrades[i].m_Cost;
            
            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(upgrades[i].m_Cost)))
            {
                upgradeHUD.transform.FindChild("UpgradeButton").GetComponent<Button>().interactable = false;
                upgradeHUD.transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                upgradeHUD.transform.FindChild("UpgradeButton").GetComponent<Button>().interactable = true;
                upgradeHUD.transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }

            m_upgradeTowerHUD.GetComponent<SelectUpgradeScript>().setSelectedTower(gameObject);
        }

        for (; i < MAX_UPGRADES; i++)
        {
            string name = "Upgrade" + i;

            GameObject upgradeHUD = m_upgradeTowerHUD.transform.FindChild(name).gameObject;

            upgradeHUD.SetActive(false);
        }

    }
}
