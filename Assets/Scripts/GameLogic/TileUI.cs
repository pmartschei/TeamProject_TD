using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    // Image and Color to indicate that a tile was drawn
    public Image m_highLight;
    public Color m_highlightColor;
    public float m_fadeMultiplier;

    // The positions for the HUD
    public GameObject m_tileSlot0;
    public GameObject m_tileSlot1;
    public GameObject m_tileSlot2;

    // The rotations for the tiles
    private Quaternion m_tileSlot0_rotation;
    private Quaternion m_tileSlot1_rotation;
    private Quaternion m_tileSlot2_rotation;

    private int m_counterCurrentTiles;
    private int m_maxTiles;
    private GameObject[] m_currentTiles;

    // The displayed Text in the game
    private Text m_displayedText;

    // Needed to access the functions of the scripts associated with that GameObject
    private GameObject m_drawTilesSystem;

    private bool m_tileAdded;

    // Use this for initialization
    void Start()
    {
        m_counterCurrentTiles = 0;
        m_maxTiles = 3;
        m_currentTiles = new GameObject[m_maxTiles];

        // Different rotations for the different positions else the tiles look off
        m_tileSlot0_rotation = Quaternion.Euler(-160, 0, 0);
        m_tileSlot1_rotation = Quaternion.Euler(-160, 0, 0);
        m_tileSlot2_rotation = Quaternion.Euler(-160, 0, 0);

        m_displayedText = GameObject.Find("TilesText").GetComponent<Text>();
        m_drawTilesSystem = GameObject.Find("DrawTileSystem");
    }

    // Update is called once per frame
    void Update()
    {
        // Enables the rotation around the z-axis
        if ((m_counterCurrentTiles > 0) && (Time.timeScale != 0))
        {
            for (int i = 0; i < m_counterCurrentTiles; i++)
            {
                m_currentTiles[i].transform.Rotate(0, 0, Mathf.PI / 4);
            }
        }

        // Flash and Fade of the Image 
        if (m_tileAdded)
        {
            m_highLight.color = m_highlightColor;
        }
        else
        {
            m_highLight.color = Color.Lerp(m_highLight.color, Color.clear, m_fadeMultiplier * Time.deltaTime);
        }

        m_tileAdded = false;
    }

    public void SetNewTile(GameObject tile)
    {
        // Copy of prefab
        m_currentTiles[m_counterCurrentTiles] = Instantiate(tile);
        m_tileAdded = true;

        // Put Tile into the right position and rotate it around the x-axis, so you see the face of the tile
        switch (m_counterCurrentTiles)
        {
            case 0:
                m_currentTiles[m_counterCurrentTiles].transform.position = m_tileSlot0.transform.position;
                m_currentTiles[m_counterCurrentTiles].transform.rotation = m_tileSlot0_rotation;
                break;

            case 1:
                m_currentTiles[m_counterCurrentTiles].transform.position = m_tileSlot1.transform.position;
                m_currentTiles[m_counterCurrentTiles].transform.rotation = m_tileSlot1_rotation;
                break;

            case 2:
                m_currentTiles[m_counterCurrentTiles].transform.position = m_tileSlot2.transform.position;
                m_currentTiles[m_counterCurrentTiles].transform.rotation = m_tileSlot2_rotation;
                break;

            default: break;
        }

        // Change the displayed text
        m_counterCurrentTiles++;
        m_displayedText.text = "" + m_counterCurrentTiles + " / " + m_maxTiles;
    }

    public void RemoveTile(GameObject tile)
    {
        // Determine the tile that was used with its y - axis
        switch ((int)tile.transform.position.x)
        {
            case 997:
                // The first tile was used -> move the second and third tile down one spot
                if (m_currentTiles[1] != null)
                {
                    m_currentTiles[1].transform.position = m_tileSlot0.transform.position;
                    m_currentTiles[1].transform.rotation = m_tileSlot0_rotation;
                    m_currentTiles[0] = m_currentTiles[1];
                    m_currentTiles[1] = null;
                }

                if (m_currentTiles[2] != null)
                {
                    m_currentTiles[2].transform.position = m_tileSlot1.transform.position;
                    m_currentTiles[2].transform.rotation = m_tileSlot1_rotation;
                    m_currentTiles[1] = m_currentTiles[2];
                    m_currentTiles[2] = null;
                }
                break;

            case 1000:
                // The second tile was used -> move the third tile down one spot
                if (m_currentTiles[2] != null)
                {
                    m_currentTiles[2].transform.position = m_tileSlot1.transform.position;
                    m_currentTiles[2].transform.rotation = m_tileSlot1_rotation;
                    m_currentTiles[1] = m_currentTiles[2];
                    m_currentTiles[2] = null;
                }
                break;

            case 1003:
                // Nothing has to be done if the third tile was used

            default: break;
        }

        Destroy(tile);

        // Change the displayed text and inform the draw script that a tile was used
        m_counterCurrentTiles--;
        m_drawTilesSystem.GetComponent<DrawTiles>().DecreaseCurrentTiles();
        m_displayedText.text = "" + m_counterCurrentTiles + " / " + m_maxTiles;
    }
}
