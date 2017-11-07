using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBody : MonoBehaviour {

    private BodyManager bm;
    private bool inTrigger;

	// Use this for initialization
	void Start ()
    {
        bm = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<BodyManager>();
        inTrigger = false;
    }
	
	// Update is called once per frame
	void Update () {
		if( inTrigger)
        {
            if (Input.GetButtonDown("Action"))
            {
                bm.SwitchToNext();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Player" )
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
