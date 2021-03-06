﻿using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    private GameObject m_buildTowerHUD;
    private GameObject m_totemHUD;
    private GameObject m_upgradeTower;

    public GameObject m_mainCamera;
    public GameObject m_villageCamera;

    public float m_cameraSpeed = 5.0f;
    public int m_cameraBorderFactor = 10;
    public float m_cameraVelocityFactor = 30.0f;

    public int m_offsetBottom = 300;
    public int m_offsetTop = 760;

    private Vector3 mousePos;

    //values for the position and rotation of the main camera
    private float m_translationX1 = -3.0f;
    private float m_translationY1 = 7.0f;
    private float m_translationZ1 = 3.5f;

    private float m_rotationX1 = 50.0f;
    private float m_rotationY1 = 80.0f;
    private float m_rotationZ1 = 0.0f;

    //values for the position and rotation of the village camera
    private float m_translationXVillage = 5.0f;
    private float m_translationYVillage = 5.0f;
    private float m_translationZVillage = -5.0f;

    private float m_rotationXVillage = 50.0f;
    private float m_rotationYVillage = 0.0f;
    private float m_rotationZVillage = 0.0f;

    public GameObject m_fieldSystem;
    private FieldScript m_fieldScript;

	// Use this for initialization
	void Start () 
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_totemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;

        m_mainCamera.transform.Translate(m_translationX1, m_translationY1, m_translationZ1);
        m_mainCamera.transform.Rotate(m_rotationX1, m_rotationY1, m_rotationZ1);
        m_mainCamera.SetActive(true);

        setVillageCamera();
        m_villageCamera.SetActive(false);

        m_fieldScript = m_fieldSystem.GetComponent<FieldScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Steuerung mit der Maus

        if (m_mainCamera.activeSelf)
        {
            mousePos = Input.mousePosition;

            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(mousePos);

            if (mousePos.x < (Screen.width / m_cameraBorderFactor) && checkYParamters()) //links
            {
                if (worldCoordinates.z > m_fieldScript.GetCameraMax())
                    return;
                float value = (Screen.width / m_cameraBorderFactor) - mousePos.x;
                float speed = m_cameraSpeed * (value / m_cameraVelocityFactor);

                if(m_buildTowerHUD.activeSelf || m_totemHUD.activeSelf || m_upgradeTower.activeSelf)
                {
                    m_buildTowerHUD.SetActive(false);
                    m_totemHUD.SetActive(false);
                    m_upgradeTower.GetComponent<SelectUpgradeScript>().Unshow();
                    m_upgradeTower.SetActive(false);
                }

                m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            }
            else if (mousePos.x > (Screen.width - (Screen.width / m_cameraBorderFactor)) && checkYParamters()) //rechts
            {
                if (worldCoordinates.z < 0)
                    return;
                float value = mousePos.x - (Screen.width - (Screen.width / m_cameraBorderFactor));
                float speed = m_cameraSpeed * (value / m_cameraVelocityFactor);

                if (m_buildTowerHUD.activeSelf || m_totemHUD.activeSelf || m_upgradeTower.activeSelf)
                {
                    m_buildTowerHUD.SetActive(false);
                    m_totemHUD.SetActive(false);
                    m_upgradeTower.GetComponent<SelectUpgradeScript>().Unshow();
                    m_upgradeTower.SetActive(false);
                }

                m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, -speed * Time.deltaTime);
            }
        }

        //Mausrad drücken
        if(Input.GetMouseButtonUp(2))
        {
            if(m_mainCamera.activeSelf)
            {
                m_mainCamera.SetActive(false);
                setVillageCamera();
                m_villageCamera.SetActive(true);
                GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_curretCam = m_villageCamera.GetComponent<Camera>();
            }
            else
            {
                m_mainCamera.SetActive(true);
                m_villageCamera.SetActive(false);
                GameObject.Find("CounterSystem").GetComponent<OverallInformation>().m_curretCam = m_mainCamera.GetComponent<Camera>();

            }
            if (m_buildTowerHUD.activeSelf || m_totemHUD.activeSelf || m_upgradeTower.activeSelf)
            {
                m_buildTowerHUD.SetActive(false);
                m_totemHUD.SetActive(false);
                m_upgradeTower.GetComponent<SelectUpgradeScript>().Unshow();
                m_upgradeTower.SetActive(false);
            }
        }


        //ALT: Steuerung mit der Tastatur
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if (m_mainCamera.activeSelf)
        //    {
        //        m_mainCamera.SetActive(false);
        //        setVillageCamera();
        //        m_villageCamera.SetActive(true);
        //    }
        //    else
        //    {
        //        m_mainCamera.SetActive(true);
        //        m_villageCamera.SetActive(false);
        //    }
        //}

        //if (m_mainCamera.activeSelf)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        //transform.Translate(new Vector3(-(m_cameraSpeed * Time.deltaTime), 0.0f, 0.0f));
        //        m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, m_cameraSpeed * Time.deltaTime);
        //    }
        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        //Vector3 currPos = m_mainCamera.transform.position;
        //        //Vector3 toAdd = new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //        //Vector3 toGoTo = currPos + toAdd;
        //        //m_mainCamera.transform.position = Vector3.Lerp(currPos, toGoTo, Time.deltaTime * 50f);
        //        m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //    }
        //}
        //else
        //{
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        m_villageCamera.transform.position += new Vector3(0.0f, 0.0f, m_cameraSpeed * Time.deltaTime);
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        m_villageCamera.transform.position += new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //    }
        //}
	}

    private void setVillageCamera()
    {
        m_villageCamera.transform.position = new Vector3(m_translationXVillage, m_translationYVillage, m_translationZVillage);
        m_villageCamera.transform.eulerAngles = new Vector3(m_rotationXVillage, m_rotationYVillage, m_rotationZVillage);
    }

    private bool checkYParamters()
    {
        if ((mousePos.y > m_offsetBottom) /*&& (mousePos.y < m_offsetTop)*/)
            return true;
        
        return false;
    }

}
