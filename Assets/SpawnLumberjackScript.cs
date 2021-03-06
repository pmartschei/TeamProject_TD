﻿using UnityEngine;
using System.Collections;

public class SpawnLumberjackScript : MonoBehaviour {

    public GameObject m_LumberjackPrefab;
    private GameObject m_Lumberjack;

    private GameObject m_infoObject;
    private OverallInformation m_infoScript;

    // Use this for initialization
    void Start()
    {
        m_infoObject = GameObject.Find("CounterSystem");
        m_infoScript = m_infoObject.GetComponent<OverallInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_infoScript.GetDeletable() && m_Lumberjack != null)
        {
            m_infoScript.SetDeletable(false);
            Destroy(m_Lumberjack);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Bla");
        Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

        if (m_infoScript.AddLumberjackPossible())
        {
            m_Lumberjack = GameObject.Instantiate(m_LumberjackPrefab);
            m_Lumberjack.transform.position = new Vector3(5, 0.25f, -2); //new Vector3(transform.position.x, transform.position.y, transform.position.z);
            m_infoScript.IncrementLumberjacks();
        }
    }
}
