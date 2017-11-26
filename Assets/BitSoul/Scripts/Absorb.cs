using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

    public float attractionForce;

    private bool absorbing;
    private GameObject target;
    private PlayerController controller;
    private float heading;
    private float direction;

    // Use this for initialization
    void Start () {
        absorbing = false;
        target = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if( absorbing )
            if( other.gameObject == target)
            {
                Destroy(target);
            }
    }

    public void absorbTarget( GameObject other )
    {
        target = other;

        heading = ( GameObject.FindGameObjectWithTag("Player").transform.position - other.transform.position ).x;
        if (heading > 0)
            direction = 1;
        else
            direction = -1;

        other.GetComponent<Rigidbody2D>().AddForce(direction * transform.right * attractionForce);
    }

    public void setAbsorbing(bool b)
    {
        absorbing = b;
    }
}
