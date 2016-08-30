using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

    public GameObject m_Lumberjack;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnMouseDown()
    {
        Debug.Log("Bla");
        Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

        GameObject obj = GameObject.Instantiate(m_Lumberjack);
        obj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    //void OnMouseOver()
    //{

    //}
}
