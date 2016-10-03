using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private GameObject[] m_pauseObjects;
    private GameObject[] m_gameOverObjects;
    private GameObject m_Panel;
    public Slider soundSlider;

    private GameObject m_buildTowerHUD;
    private GameObject m_totemHUD;
    private GameObject m_upgradeTower;
    public GameObject m_checkTextField;
    public GameObject m_waveText;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        m_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        m_gameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        m_Panel = GameObject.FindGameObjectWithTag("ShowOnPauseAndGameOver");
        hidePaused();

        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_totemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;
        //m_checkTextField = GameObject.Find("HUDCanvas").transform.Find("CheckTextField").gameObject;
        //m_waveText = GameObject.Find("HUDCanvas").transform.Find("WaveTextBox").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void pauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;

            m_checkTextField.SetActive(false);
            m_buildTowerHUD.SetActive(false);
            m_totemHUD.SetActive(false);
            m_upgradeTower.SetActive(false);

            showPaused();
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void showPaused()
    {
        m_Panel.SetActive(true);

        foreach (GameObject g in m_pauseObjects)
            g.SetActive(true);

        foreach (GameObject g in m_gameOverObjects)
            g.SetActive(false);

        m_waveText.SetActive(false);
    }

    public void hidePaused()
    {
        m_Panel.SetActive(false);

        foreach (GameObject g in m_pauseObjects)
            g.SetActive(false);
       

        m_waveText.SetActive(true);
    }

    /*
    public void adjustSound()
    {
        AudioListener.volume = soundSlider.value;
    }
    */

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
