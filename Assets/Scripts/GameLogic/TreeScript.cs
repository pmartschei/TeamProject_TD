using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

    bool isClicked;

	// Use this for initialization
	void Start () 
    {
        isClicked = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            Debug.Log("Klick");
        }
	}
}
