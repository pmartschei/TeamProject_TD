using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    // The positions for the HUD
    public GameObject m_TileSlot0;
    public GameObject m_TileSlot1;
    public GameObject m_TileSlot2;

    private int m_counterCurrentTiles;
    private int m_maxTiles;
    private GameObject[] m_currentTiles;

    // The displayed Text in the game
    private Text m_displayedText;

    // Needed to access the functions of the scripts associated with that GameObject
    private GameObject m_drawTilesSystem;

    // Use this for initialization
    void Start ()
    {
        m_counterCurrentTiles = 0;
        m_maxTiles = 3;
        m_currentTiles = new GameObject[m_maxTiles];

        m_displayedText = GameObject.Find("TextSavedTiles").GetComponent<Text>();
        m_drawTilesSystem = GameObject.Find("DrawTileSystem");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Enables the rotation around the z-axis
	    if(m_counterCurrentTiles > 0)
        {
            for(int i = 0; i < m_counterCurrentTiles; i++)
            {
                m_currentTiles[i].transform.Rotate(0, 0, Mathf.PI / 4);
            }
        }
	}

    public void SetNewTile(GameObject tile)
    {
        // Copy of prefab
        m_currentTiles[m_counterCurrentTiles] = Instantiate(tile);

        // Put Tile into the right position and rotate it around the x-axis, so you see the face of the tile
        switch (m_counterCurrentTiles)
        {
            case 0: m_currentTiles[m_counterCurrentTiles].transform.position = m_TileSlot0.transform.position;
                    m_currentTiles[m_counterCurrentTiles].transform.Rotate(-90, 0, 0);
                    m_currentTiles[m_counterCurrentTiles].AddComponent<UISelectTile>();
                    break;

            case 1: m_currentTiles[m_counterCurrentTiles].transform.position = m_TileSlot1.transform.position;
                    m_currentTiles[m_counterCurrentTiles].transform.Rotate(-90, 0, 0);
                    m_currentTiles[m_counterCurrentTiles].AddComponent<UISelectTile>();
                    break;

            case 2: m_currentTiles[m_counterCurrentTiles].transform.position = m_TileSlot2.transform.position;
                    m_currentTiles[m_counterCurrentTiles].transform.Rotate(-90, 0, 0);
                    m_currentTiles[m_counterCurrentTiles].AddComponent<UISelectTile>();
                    break;

            default: break;
        }

        // Change the displayed text
        m_counterCurrentTiles++;
        m_displayedText.text = "Saved Tiles\n" + m_counterCurrentTiles + " / " + m_maxTiles;
    }

    public void RemoveTile(GameObject tile)
    {
        // Change the displayed text and inform the draw script that a tile was used
        m_counterCurrentTiles--;
        m_drawTilesSystem.GetComponent<DrawTiles>().DecreaseCurrentTiles();
        m_displayedText.text = "Saved Tiles\n" + m_counterCurrentTiles + " / " + m_maxTiles;
    }
}
