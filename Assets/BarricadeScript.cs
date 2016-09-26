using UnityEngine;
using System.Collections;

public class BarricadeScript : MonoBehaviour {

    public GameObject m_cloud;
    public GameObject m_dust;
    public GameObject m_fieldObj;

    private FieldScript m_field;

    private GameObject m_cloud1, m_cloud2, m_storm;

	// Use this for initialization
	void Start () 
    {
        m_field = m_fieldObj.GetComponent<FieldScript>();

        m_cloud1 = null;
        m_cloud2 = null;
        m_storm = null;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.M))
            drawBarricade();
	}

    public void drawBarricade()
    {
        reset();

        float max = m_field.GetCameraMax();
        max += 4;

        m_cloud1 = GameObject.Instantiate(m_cloud);
        m_cloud1.transform.position = new Vector3(3.0f, 1.0f, max);

        m_cloud2 = GameObject.Instantiate(m_cloud);
        m_cloud2.transform.position = new Vector3(8.0f, 1.0f, max);

        m_storm = GameObject.Instantiate(m_dust);
        m_storm.transform.position = new Vector3(5.5f, 1.0f, max);
    }

    private void reset()
    {
        if (m_cloud1 != null)
        {
            GameObject.Destroy(m_cloud1);
            m_cloud1 = null;
        }
        if(m_cloud2 != null)
        {
            GameObject.Destroy(m_cloud2);
            m_cloud2 = null;
        }
        if(m_storm != null)
        {
            GameObject.Destroy(m_storm);
            m_storm = null;
        }
    }
}
