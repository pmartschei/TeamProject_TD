using UnityEngine;
using System.Collections;

public class BulletCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        Destroy(coll.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
