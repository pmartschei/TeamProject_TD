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

    private GameObject m_upgradeButton;
    private GameObject m_upgradeCircle;

    private Transform m_target;
    private Camera m_camera;

    private LineRenderer m_line;
    public int m_segments = 20;
    public float m_zValue = 3.0f;
    public float m_xRadius = 100.0f;
    public float m_yRadius = 100.0f;
    public float m_angle = 2.0f;

    // Use this for initialization
    void Start()
    {
        m_upgradeTowerHUD = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_camera = Camera.main;
        m_target = gameObject.transform;

        m_line = GetComponent<LineRenderer>();
        m_line.SetVertexCount(0);
        m_line.useWorldSpace = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_upgradeTowerHUD.SetActive(false);
                unshowProps();
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
                TowerScript tw = GetComponent<TowerScript>();
                if (tw == null) return;
                Upgrade[] upgrades = tw.GetNextUpgrades();

                showUpgrades(upgrades);
            }

            showProps();

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

            transform.anchoredPosition = new Vector2(x*UPGRADE_DISTANCE, (y*UPGRADE_DISTANCE));

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

    public void showProps()
    {
        m_line.SetVertexCount(m_segments + 1);

        float towerRadius = 0.0f;

        TowerScript ts = GetComponent<TowerScript>();
        if (ts != null)
        {
            towerRadius = ts.Radius;
        }


        if (towerRadius == 0.0f) towerRadius = 1.0f;

        float x, y;
        m_line.SetWidth(m_zValue, m_zValue);

        for (int i = 0; i < (m_segments + 1); ++i)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * m_angle) * towerRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * m_angle) * towerRadius;

            m_line.SetPosition(i, new Vector3(x, m_zValue, y));

            m_angle += (360.0f / m_segments);
        }
    }
    public void unshowProps()
    {
        m_line.SetVertexCount(0);
    }
}
