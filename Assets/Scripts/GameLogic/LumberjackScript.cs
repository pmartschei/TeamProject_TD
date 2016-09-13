using UnityEngine;
using System.Collections;

public class LumberjackScript : MonoBehaviour {

    private Animator anim;

    private GameObject m_infoObject;
    private OverallInformation m_infoScript;

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();

        m_infoObject = GameObject.Find("CounterSystem");
        m_infoScript = m_infoObject.GetComponent<OverallInformation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //anim.Play("Turn");

        if(transform.position.y < -20)
        {
            Destroy(gameObject);
            m_infoScript.DecrementLumberjacks();
            m_infoScript.SetDeletable(false);
        }
	}
}
