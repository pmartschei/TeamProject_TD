using UnityEngine;
using System.Collections;

public class LumberjackScript : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //anim.Play("Turn");

        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
	}
}
