using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTowerScript : MonoBehaviour 
{
    public GameObject m_buildTowerHUD;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void buildTower()
    {
        Debug.Log("Build Tower");
        m_buildTowerHUD.SetActive(false);
    }
}
