using UnityEngine;
using System.Collections;

public class CreateFloorScript : MonoBehaviour {

    public GameObject m_bottomPlane;
    public GameObject m_backPlane;
    public GameObject m_cloud;

    private GameObject m_bot;
    private GameObject m_bot2;
    private GameObject m_back;

	// Use this for initialization
	void Start () 
    {
        m_bot = null;
        m_bot = GameObject.Instantiate(m_bottomPlane);
        m_bot.transform.position = new Vector3(0.0f, -5.0f, 0.0f);
        m_bot.transform.localScale = new Vector3(1.0f, 3.0f, 1.0f);

        m_bot2 = null;
        m_bot2 = GameObject.Instantiate(m_bottomPlane);
        m_bot2.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
        m_bot2.transform.localScale = new Vector3(1.0f, 3.0f, 1.0f);

        m_back = null;
        m_back = GameObject.Instantiate(m_backPlane);
        m_back.transform.position = new Vector3(20.0f, 3.0f, 0.0f);

        int r = 0;
        float off = 0.0f;
        for(int i = -10; i < 1000; ++i)
        {
            r = Random.Range(0, 100);
            if (r < 33)
                off = 0;
            else if (r < 66)
                off = 0.5f;
            else
                off = -0.5f;
            GameObject c = GameObject.Instantiate(m_cloud);
            c.transform.position = new Vector3(10.0f, 2.0f, i * 3.0f + off);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void expandBG()
    {
        Vector3 expandX = new Vector3(10.0f, 0.0f, 0.0f);
        Vector3 expandY = new Vector3(0.0f, 10.0f, 0.0f);

        Vector3 scale = m_back.transform.localScale;
        Vector3 newScale = scale + expandX;
        m_back.transform.localScale = newScale;

        scale = m_bot.transform.localScale;
        newScale = scale + expandY;
        m_bot.transform.localScale = newScale;

        scale = m_bot2.transform.localScale;
        newScale = scale + expandY;
        m_bot2.transform.localScale = newScale;
    }
}
