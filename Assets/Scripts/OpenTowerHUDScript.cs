using UnityEngine;
using System.Collections;

public class OpenTowerHUDScript: MonoBehaviour 
{
    private GameObject m_buildTowerHUD;
    private Transform m_target;
    private Camera m_camera;

	// Use this for initialization
	void Start () 
    {
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.Find("BuildTowerHUD").gameObject;
        m_target = gameObject.transform;
        m_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnMouseDown()
    {
        Debug.Log("TowerBuildHUD");
        Vector3 screenPos = m_camera.WorldToScreenPoint(m_target.position);
        screenPos += new Vector3(0.0f, 10.0f, 0.0f);
        m_buildTowerHUD.transform.position = screenPos;
        m_buildTowerHUD.SetActive(true);  
    }
}
