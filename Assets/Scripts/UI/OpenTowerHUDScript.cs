﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpenTowerHUDScript : MonoBehaviour
{
    private GameObject m_lifeAndMoneySystem;

    private GameObject m_buildTowerHUD;
    private int m_firstTowerCost;
    private int m_secondTowerCost;
    private int m_thirdTowerCost;
    private int m_fourthTowerCost;

    private Transform m_target;
    private Camera m_camera;

    private Vector3 m_screenpos;

    // Use this for initialization
    // Get the HUD and MoneySystem
    // Get the amount for each tower
    // Get the main Camera and the position of the tile for the correct view
    void Start()
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");

        m_firstTowerCost = int.Parse(m_buildTowerHUD.transform.FindChild("UpperLeftTower").transform.FindChild("TowerCostText").GetComponent<Text>().text);
        m_secondTowerCost = int.Parse(m_buildTowerHUD.transform.FindChild("UpperRightTower").transform.FindChild("TowerCostText").GetComponent<Text>().text);
        m_thirdTowerCost = int.Parse(m_buildTowerHUD.transform.FindChild("LowerLeftTower").transform.FindChild("TowerCostText").GetComponent<Text>().text);
        m_fourthTowerCost = int.Parse(m_buildTowerHUD.transform.FindChild("LowerRightTower").transform.FindChild("TowerCostText").GetComponent<Text>().text);

        m_target = gameObject.transform;
        m_camera = Camera.main;
    }

    // Update is called once per frame
    // Turn off HUD if the Escape Key is pressed
    // Check for every tower option if enough money is available to build it, if not decrease alpha values and disable the button
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
                m_buildTowerHUD.SetActive(false);


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_firstTowerCost)))
            {
                m_buildTowerHUD.transform.FindChild("UpperLeftTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = false;
                m_buildTowerHUD.transform.FindChild("UpperLeftTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_buildTowerHUD.transform.FindChild("UpperLeftTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = true;
                m_buildTowerHUD.transform.FindChild("UpperLeftTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_secondTowerCost)))
            {
                m_buildTowerHUD.transform.FindChild("UpperRightTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = false;
                m_buildTowerHUD.transform.FindChild("UpperRightTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_buildTowerHUD.transform.FindChild("UpperRightTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = true;
                m_buildTowerHUD.transform.FindChild("UpperRightTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_thirdTowerCost)))
            {
                m_buildTowerHUD.transform.FindChild("LowerLeftTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = false;
                m_buildTowerHUD.transform.FindChild("LowerLeftTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_buildTowerHUD.transform.FindChild("LowerLeftTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = true;
                m_buildTowerHUD.transform.FindChild("LowerLeftTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }


            if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_fourthTowerCost)))
            {
                m_buildTowerHUD.transform.FindChild("LowerRightTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = false;
                m_buildTowerHUD.transform.FindChild("LowerRightTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
            }
            else
            {
                m_buildTowerHUD.transform.FindChild("LowerRightTower").transform.FindChild("TowerButton").GetComponent<Button>().interactable = true;
                m_buildTowerHUD.transform.FindChild("LowerRightTower").transform.FindChild("CircleImage").GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        else
        {
            m_buildTowerHUD.SetActive(false);
        }
    }

    // position the hud at the right place
    // transfer the position of the current tile to SelectTowerScript
    public void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            if (!GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject.activeSelf)
            {
                m_screenpos = m_camera.WorldToScreenPoint(m_target.position);
                m_screenpos += new Vector3(0.0f, 10.0f, 0.0f);
                m_buildTowerHUD.transform.position = m_screenpos;
                m_buildTowerHUD.SetActive(true);

                // GameObject.Find("HUDCanvas").transform.FindChild("UpgradeTowerHUD").FindChild("Destroy").GetComponent<SelectUpgradeScript>().setSelectedTile(gameObject);

                m_buildTowerHUD.transform.FindChild("UpperLeftTower").GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
                m_buildTowerHUD.transform.FindChild("UpperRightTower").GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
                m_buildTowerHUD.transform.FindChild("LowerLeftTower").GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
                m_buildTowerHUD.transform.FindChild("LowerRightTower").GetComponent<SelectTowerScript>().setPosition(m_target.position, gameObject);
            }
        }    
    }
}
