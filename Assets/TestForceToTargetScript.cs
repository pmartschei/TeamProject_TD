using UnityEngine;
using System.Collections;

public class TestForceToTargetScript : MonoBehaviour {

    public GameObject target;
	// Use this for initialization
	void Start () {
        Rigidbody r = this.GetComponent<Rigidbody>();
        r.AddForce(Vector3.up*250);
	}

    void OnCollisionEnter(Collision obj)
    {
        Destroy(this);
    }
	// Update is called once per frame
    void Update()
    {
        Vector3 diff = this.transform.position - target.transform.position;
        Rigidbody r = this.GetComponent<Rigidbody>();
        diff.Normalize();
        r.AddForce(-diff*4);
	}
}
