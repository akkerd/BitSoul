using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour {

    public float attractionDistance;
    public LayerMask whatIsTarget;
    public float attractionForce;

    private Absorb absorb;
    private GameObject clone;
    private float heading;
    private float direction;

    // Use this for initialization
    void Start ()
    {
        absorb = GetComponentInChildren<Absorb>();
    }
	
	// Update is called once per frame
	void Update () {

        // If player is in absortion range...
        if (Physics2D.OverlapCircle(transform.position, attractionDistance, whatIsTarget))
        {
            //... check if the player has the merging button pressed and if so...
			if (Input.GetButton("Merge"))
            {
                // Enable clone/player collision
                Physics2D.IgnoreLayerCollision(11, 9, false);

                //... enable absorbtion
                absorb.setAbsorbing(true);
                // Find clone's GameObject
                clone = Physics2D.OverlapCircle(transform.position, attractionDistance, whatIsTarget).gameObject;
                
                // Attract the player 
                if( clone )
                {
                    heading = (transform.position - clone.transform.position).x;
                    if (heading > 0)
                        direction = 1;
                    else
                        direction = -1;

                    clone.GetComponent<Rigidbody2D>().AddForce(direction * transform.right * attractionForce);
                }
            }
            else
            {
                // Disable clone/player collision
                Physics2D.IgnoreLayerCollision(11, 9, true);
            }
        }

        // If merging button is released, cancel absorb
        if (Input.GetButtonUp("Merge"))
        {
            absorb.setAbsorbing(false);
        }
    }
}
