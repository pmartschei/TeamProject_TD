using UnityEngine;
using System.Collections;

public class TestShootScript : MonoBehaviour {

    public GameObject m_bullet;
    public GameObject m_target;
    private float m_Timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        m_Timer += Time.deltaTime;
        if (m_Timer > 1)
        {
            GameObject obj=Instantiate(m_bullet);
            TestForceToTargetScript s= obj.GetComponent<TestForceToTargetScript>();
            s.target = m_target;
            m_Timer = 0;
        }
	}
}
