using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

    private GameObject m_infoObject;
    private OverallInformation m_infoScript;

    private Color m_chopDownColor;

    private bool m_isChopped;
    private float m_startTime;
    private float m_interval;

	// Use this for initialization
	void Start () 
    {
        m_infoObject = GameObject.Find("CounterSystem");
        m_infoScript = m_infoObject.GetComponent<OverallInformation>();

        m_chopDownColor = new Color(0.2f, 1.0f, 0.0f);

        m_isChopped = false;
        m_interval = 3.0f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(m_isChopped)
        {
            float curr = Time.time - m_startTime;
            if(curr > m_interval)
            {
                m_infoScript.DecrementLumberjacks();
                Destroy(gameObject);
            }
        }
	}

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("enter");
    //}

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = m_chopDownColor;

            m_infoScript.SetDeletable(true);

            m_isChopped = true;
            m_startTime = Time.time;
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("exit");
    //}
}
