using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

    public GameObject m_Lumberjack;

    private GameObject m_infoObject;
    private OverallInformation m_infoScript;

	// Use this for initialization
	void Start () 
    {
        m_infoObject = GameObject.Find("CounterSystem");
        m_infoScript = m_infoObject.GetComponent<OverallInformation>();
    }
	
	// Update is called once per frame
	void Update () 
    {
 
	}

    void OnMouseDown()
    {
        Debug.Log("Bla");
        Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

        if (m_infoScript.AddLumberjackPossible())
        {
            GameObject obj = GameObject.Instantiate(m_Lumberjack);
            obj.transform.position = new Vector3(5, 3, -2); //new Vector3(transform.position.x, transform.position.y, transform.position.z);
            m_infoScript.IncrementLumberjacks();
        }
    }

    //void OnMouseOver()
    //{

    //}
}
