using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawTiles : MonoBehaviour
{
    // Drawing your first Tile after X seconds
    public float m_startDrawing;
    // Draw a new Tile every X seconds
    public float m_drawNextTile;

    // GameObjects for the Tiles
    public GameObject m_blankTile;
    public GameObject m_roadTileStraight;
    public GameObject m_roadTileCurve;
    public GameObject m_buildTile;

    // GameObjects for the Saved Tiles
    private GameObject[] m_savedTiles;

    // Chance to get one of the following Tiles each draw
    private float m_getBlank;
    private float m_getRoad;
    private float m_getBuildSpace;

    // Used to change the percentages for the next drawn Tile to try and equal the amount of each drawn Tile among all three
    private float m_changePercentagesFactor;

    // Determines which Tile is drawn
    private float m_rndNumber;

    // Used to set up the proper percentages after the first Tile is drawn
    private bool m_firstTile;

    // Used to determine the time spend in the Main Menu
    private float m_timeSpendInMenu;

    // Maximum amount of Tiles you are allowed to save at one time
    private int m_maxTiles;

    // Array of saved Tiles
    private GameObject[] m_tiles;

    // The current number of saved Tiles
    private int m_currentTiles;

    // The displayed Text in the game
    private Text m_displayedText;

    // Use this for initialization
    void Start ()
    {
        // Only start the script and therefor the drawing process after the Main Menu is over and the correct Scene is loaded
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("testScene"))
        {
            // Get the time spend in the Main Menu (needed for the correct drawing intervalls)
            m_timeSpendInMenu = Time.time;

            // First Tile is never a Blank because thats boring for the player
            // getBuildSpace ist immer gleich getRoad und kann nachher entfernt werden, habs der Übersicht halber jetzt noch drin gelassen
            m_getRoad = 0.5f;
            m_getBuildSpace = 0.5f;
            m_getBlank = 0.0f;

            // Always add or subtract 25% of the 33% Chance to draw that Tile
            // ToDo: Balance it properly
            m_changePercentagesFactor = 0.33f * 0.25f;

            m_firstTile = true;

            // Initialize variables for the saved Tiles
            m_maxTiles = 3;
            m_currentTiles = 0;

            m_tiles = new GameObject[m_maxTiles];
            m_savedTiles = new GameObject[m_maxTiles];

            for (int i = 0; i < m_maxTiles; i++)
            {
                m_tiles[i] = new GameObject();
                m_savedTiles[i] = GameObject.Find("SavedTile" + (i + 1));
                m_savedTiles[i].SetActive(false);
            }

            m_displayedText = GameObject.Find("TextSavedTiles").GetComponent<Text>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((Time.time - m_timeSpendInMenu) > m_startDrawing)
        {
            m_startDrawing = (Time.time - m_timeSpendInMenu) + m_drawNextTile;

            // rndNumber between 0.0f and 1.0f (both included)
            m_rndNumber = Random.Range(0.0f, 1.0f);

            if (m_rndNumber >= m_getRoad)
            {
                // Set up the correct and equal chances after the first Tile
                // Else calculate the new percentages to get an equal spread amongst all Tiles
                if (m_firstTile)
                {
                    m_getRoad = 0.66f;
                    m_getBuildSpace = 0.66f;
                    m_getBlank = 0.33f;
                    m_firstTile = false;
                }
                else
                {
                    if (m_currentTiles < 3)
                    {
                        m_savedTiles[m_currentTiles].SetActive(true);
                        m_currentTiles++;
                        m_displayedText.text = "Saved Tiles\n" + m_currentTiles + " / 3";

                        // The range between getRoad and 1.0f gets smaller, less chance to draw that Tile
                        // The range between getBuildSpace and getBlank gets bigger, more chance to draw that Tile
                        // The range between 0.0f and getBlank gets bigger, more chance to draw that Tile
                        // Example: 
                        /*
                            *      Before        |     After
                            *      1.00 - 0.66   |     1.00 - 0.74
                            *      0.66 - 0.33   |     0.74 - 0.37    
                            *      0.33 - 0.00   |     0.37 - 0.00
                            */
                        m_getRoad += m_changePercentagesFactor;
                        m_getBuildSpace = m_getRoad;
                        m_getBlank += (m_changePercentagesFactor / 2);

                        Debug.Log("Road - Tile");
                    }
                }
            }
            else if (m_rndNumber < m_getBuildSpace && m_rndNumber >= m_getBlank)
            {
                // Set up the correct and equal chances after the first Tile
                // Else calculate the new percentages to get an equal spread amongst all Tiles
                if (m_firstTile)
                {
                    m_getRoad = 0.66f;
                    m_getBuildSpace = 0.66f;
                    m_getBlank = 0.33f;
                    m_firstTile = false;
                }
                else
                {
                    if (m_currentTiles < 3)
                    {
                        m_savedTiles[m_currentTiles].SetActive(true);
                        m_currentTiles++;
                        m_displayedText.text = "Saved Tiles\n" + m_currentTiles + " / 3";

                        // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                        // The range between getBuildSpace and getBlank gets smaller, less chance to draw that Tile
                        // The range between 0.0f and getBlank gets bigger, more chance to draw that Tile
                        m_getRoad -= (m_changePercentagesFactor / 2);
                        m_getBuildSpace = m_getRoad;
                        m_getBlank += (m_changePercentagesFactor / 2);

                        Debug.Log("Build - Tile");
                    }
                }
            }
            else
            {
                if (m_currentTiles < 3)
                {
                    m_savedTiles[m_currentTiles].SetActive(true);
                    m_currentTiles++;
                    m_displayedText.text = "Saved Tiles\n" + m_currentTiles + " / 3";

                    // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                    // The range between getBuildSpace and getBlank gets bigger, more chance to draw that Tile
                    // The range between 0.0f and getBlank gets smaller, less chance to draw that Tile
                    m_getRoad -= (m_changePercentagesFactor / 2);
                    m_getBuildSpace = m_getRoad;
                    m_getBlank -= m_changePercentagesFactor;

                    Debug.Log("Blank - Tile");
                }
            }
        }
    }

    public void DecreaseCurrentTiles()
    {
        m_currentTiles--;
    }
}
