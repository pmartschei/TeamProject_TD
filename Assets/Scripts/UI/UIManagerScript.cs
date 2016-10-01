using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class UIManagerScript : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
