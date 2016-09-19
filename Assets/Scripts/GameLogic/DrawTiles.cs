using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawTiles : MonoBehaviour
{
    // Drawing your first Tile after X seconds
    public float m_drawNextTile;
    // Draw a new Tile every X seconds
    public float m_drawIntervall;

    // Stores all forms of Tiles
    public GameObject m_tileSystem;

    // Needed to access the functions of the scripts associated with that GameObject
    private GameObject m_drawTileSystem;

    // Chance to get one of the following Tiles each draw
    private float m_getBlank;
    private float m_getRoad;

    // Used to change the percentages for the next drawn Tile to try and equal the amount of each drawn Tile among all three
    private float m_changePercentagesFactor;

    // Determines which Tile is drawn
    private float m_rndNumber;

    // Used to determine the time spend in the Main Menu
    private float m_timeSpendInMenu;

    // Maximum amount of Tiles you are allowed to save at one time
    private int m_maxTiles;

    // The current number of saved Tiles
    private int m_counterCurrentTiles;


    // Use this for initialization
    void Start()
    {
        // Only start the script and therefor the drawing process after the Main Menu is over and the correct Scene is loaded

        GameObject.Find("BuildTowerHUD").SetActive(false);
        GameObject.Find("UpgradeTowerHUD").SetActive(false);

        // Get the time spend in the Main Menu (needed for the correct drawing intervalls)
        m_timeSpendInMenu = Time.time;

        // Set up the initial percantages
        m_getRoad = 0.66f;
        m_getBlank = 0.33f;

        // Always add or subtract 25% of the 33% Chance to draw that Tile
        m_changePercentagesFactor = 0.33f * 0.25f;

        // Initialize variables for the saved Tiles
        m_maxTiles = 3;
        m_counterCurrentTiles = 0;

        m_drawTileSystem = GameObject.Find("DrawTileSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - m_timeSpendInMenu) > m_drawNextTile)
        {
            m_drawNextTile = (Time.time - m_timeSpendInMenu) + m_drawIntervall;

            // rndNumber between 0.0f and 1.0f (both included)
            m_rndNumber = Random.Range(0.0f, 1.0f);
            
            if (m_rndNumber >= m_getRoad)
            {
                if (m_counterCurrentTiles < m_maxTiles)
                {
                    int rndRoadTile = Random.Range(1, 5);
                    switch (rndRoadTile)
                    {
                        case 1:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_StreetStraightVar1);
                            break;
                        case 2:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_StreetStraightVar2);
                            break;
                        case 3:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_StreetCurveVar1);
                            break;
                        case 4:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_StreetCurveVar2);
                            break;
                        default:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_StreetStraightVar1);
                            break;
                    }

                    m_counterCurrentTiles++;

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
                    m_getBlank += (m_changePercentagesFactor / 2);
                }
            }
            else if (m_rndNumber < m_getRoad && m_rndNumber >= m_getBlank)
            {
                if (m_counterCurrentTiles < 3)
                {
                    int rndBuildTile = Random.Range(1, 2);

                    switch (rndBuildTile)
                    {
                        case 1:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_TowerBuildSpotVar1);
                            break;
                        case 2:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_TowerBuildSpotVar2);
                            break;
                        default:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_TowerBuildSpotVar1);
                            break;
                    }
                    m_counterCurrentTiles++;

                    // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                    // The range between getBuildSpace and getBlank gets smaller, less chance to draw that Tile
                    // The range between 0.0f and getBlank gets bigger, more chance to draw that Tile
                    m_getRoad -= (m_changePercentagesFactor / 2);
                    m_getBlank += (m_changePercentagesFactor / 2);
                }
            }
            else
            {
                if (m_counterCurrentTiles < 3)
                {
                    int rndBlankTile = Random.Range(1, 2);

                    switch (rndBlankTile)
                    {
                        case 1:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_BlankVar1);
                            break;
                        case 2:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_BlankVar2);
                            break;
                        default:
                            m_drawTileSystem.GetComponent<TileUI>().SetNewTile(m_tileSystem.GetComponent<TileSystem>().m_BlankVar1);
                            break;
                    }

                    m_counterCurrentTiles++;

                    // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                    // The range between getBuildSpace and getBlank gets bigger, more chance to draw that Tile
                    // The range between 0.0f and getBlank gets smaller, less chance to draw that Tile
                    m_getRoad -= (m_changePercentagesFactor / 2);
                    m_getBlank -= m_changePercentagesFactor;
                }
            }
        }
    }

    public void DecreaseCurrentTiles(int amount)
    {
        m_counterCurrentTiles -= amount;
    }
}
