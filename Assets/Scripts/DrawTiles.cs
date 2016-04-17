using UnityEngine;
using System.Collections;

public class DrawTiles : MonoBehaviour
{
    // Drawing your first Tile after X seconds
    private float startDrawing;
    // Draw a new Tile every X seconds
    private float drawNextTile;

    // Chance to get one of the following Tiles each draw
    private float getBlank;
    private float getRoad;
    private float getBuildSpace;

    // Used to change the percentages for the next drawn Tile to try and equal the amount of each drawn Tile among all three
    private float changePercentagesFactor;

    // Determines which Tile is drawn
    private float rndNumber;

    // Used to set up the proper percentages after the first Tile is drawn
    private bool firstTile;

    // Use this for initialization
    void Start ()
    {
        startDrawing = 0.0f;
        drawNextTile = 2.0f;

        // First Tile is never a Blank because thats boring for the player
        // getBuildSpace ist immer gleich getRoad und kann nachher entfernt werden, habs der Übersicht halber jetzt noch drin gelassen
        getRoad = 0.5f;
        getBuildSpace = 0.5f;
        getBlank = 0.0f;

        // Always add or subtract 25% of the 33% Chance to draw that Tile
        // ToDo: Balance it properly
        changePercentagesFactor = 0.33f * 0.25f;

        firstTile = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > startDrawing)
        {
            startDrawing = Time.time + drawNextTile;

            // rndNumber between 0.0f and 1.0f (both included)
            rndNumber = Random.Range(0.0f, 1.0f);

            if(rndNumber >= getRoad)
            {
                // Set up the correct and equal chances after the first Tile
                // Else calculate the new percentages to get an equal spread amongst all Tiles
                if(firstTile)
                { 
                    getRoad = 0.66f;
                    getBuildSpace = 0.66f;
                    getBlank = 0.33f;
                    firstTile = false;
                }
                else
                {
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
                    getRoad += changePercentagesFactor;
                    getBuildSpace = getRoad;
                    getBlank += (changePercentagesFactor / 2); 
                }

                // ToDo: Add the real function that generates one of the multiple Road Tiles
                Debug.Log("Road - Tile");
            }
            else if(rndNumber < getBuildSpace && rndNumber >= getBlank)
            {
                // Set up the correct and equal chances after the first Tile
                // Else calculate the new percentages to get an equal spread amongst all Tiles
                if (firstTile)
                {
                    getRoad = 0.66f;
                    getBuildSpace = 0.66f;
                    getBlank = 0.33f;
                    firstTile = false;
                }
                else
                {
                    // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                    // The range between getBuildSpace and getBlank gets smaller, less chance to draw that Tile
                    // The range between 0.0f and getBlank gets bigger, more chance to draw that Tile
                    getRoad -= (changePercentagesFactor / 2);
                    getBuildSpace = getRoad;
                    getBlank += (changePercentagesFactor / 2);
                }

                // ToDo: Add the real function that generates a Build Tile
                Debug.Log("Build - Tile");
            }
            else
            {
                // The range between getRoad and 1.0f gets bigger, more chance to draw that Tile
                // The range between getBuildSpace and getBlank gets bigger, more chance to draw that Tile
                // The range between 0.0f and getBlank gets smaller, less chance to draw that Tile
                getRoad -= (changePercentagesFactor / 2);
                getBuildSpace = getRoad;
                getBlank -= changePercentagesFactor;

                // ToDo: Add the real function that generates a Blank Tile
                Debug.Log("Blank - Tile");
            }
        }
    }
}
