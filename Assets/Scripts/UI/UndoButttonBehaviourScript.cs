using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UndoButttonBehaviourScript : MonoBehaviour
{
    public GameObject m_undoButton;
    public int m_undoCost;
    private GameObject m_lifeAndMoneySystem;

    // Use this for initialization
    void Start ()
    {
        m_lifeAndMoneySystem = GameObject.Find("LifeAndMoneySystem");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!(m_lifeAndMoneySystem.GetComponent<LifeAndMoneyScript>().isMoneyDecreasePossible(m_undoCost)))
        {
            m_undoButton.GetComponent<Button>().interactable = false;
            m_undoButton.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.6f);
        }
        else
        {
            m_undoButton.GetComponent<Button>().interactable = true;
            m_undoButton.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
