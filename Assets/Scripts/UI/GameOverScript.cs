using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject m_EnemySystem;
    public GameObject m_Panel;
    public GameObject[] m_GameOverObjects;
    public GameObject[] m_PauseGameObjects;

    public GameObject m_checkTextBox;
    public GameObject m_waveTextBox;

    // Use this for initialization
    void Start ()
    {
        m_EnemySystem = GameObject.Find("EnemySystem");
        /*m_Panel = GameObject.FindGameObjectWithTag("ShowOnPauseAndGameOver");
        m_GameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        m_PauseGameObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");*/
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void activate()
    {
        Time.timeScale = 0;

        m_checkTextBox.SetActive(false);
        m_waveTextBox.SetActive(false);

        m_Panel.SetActive(true);

        foreach (GameObject g in m_GameOverObjects)      
            g.SetActive(true);

        foreach (GameObject g in m_PauseGameObjects)
            g.SetActive(false);

        int waveNumber = m_EnemySystem.GetComponent<SpawnEnemyScript>().getCurrentWaveIndex();
        if(waveNumber != 1)
            gameObject.transform.FindChild("GameOverText").GetComponent<Text>().text = "Game Over\n\nYou survived " + waveNumber + " Waves!";
        else
            gameObject.transform.FindChild("GameOverText").GetComponent<Text>().text = "Game Over\n\nYou survived " + waveNumber + " Wave!";
    }
}
