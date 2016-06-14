using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private GameObject[] m_pauseObjects;
    public Slider soundSlider;

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
        m_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
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
        foreach(GameObject g in m_pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach(GameObject g in m_pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void adjustSound()
    {
        AudioListener.volume = soundSlider.value;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false; // Nur zum testen, löschen später!!!
        Application.Quit();
    }
}
