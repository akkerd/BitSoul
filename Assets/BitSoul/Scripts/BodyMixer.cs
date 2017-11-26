using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMixer : MonoBehaviour {

    public float absortionDistance;
    public LayerMask whatIsTarget;

    private Vector3 forward;
    private GameObject player;
    private bool wRay;
    private Absorb absorb;
    private IDictionary<string, Power> powersDic;
    private LinkedList<string> activePowers;
    private PlayerController controller;
    
    private float direction;
    private bool playerInZone;
    
    void Start () {
        // Find secondary classes
        absorb = GetComponentInChildren<Absorb>();
        controller = GetComponent<PlayerController>();

        //Initialize powers' dictionary
        Power white = new Power(1, new Color(1, 1, 1, 1), 1, 1, 1);
        Power cyan = new Power(3, new Color(0, 1, 1, 1), 1.5f, 1, 1);
        Power magenta = new Power(5, new Color(1, 0, 1, 1), 0.5f, 1.5f, 1.5f);
        Power yellow = new Power(7, new Color(1, 1, 0, 1), 1, 0.5f, 0.5f);
        Power blue = new Power(15, new Color(0, 0, 1, 1), 1, 1.5f, 1.5f);
        Power green = new Power(21, new Color(0, 1, 0, 1), 1.5f, 0.5f, 0.5f);
        Power red = new Power(35, new Color(1, 0, 0, 1), 1, 1, 1);
        Power black = new Power(105, new Color(0, 0, 0, 1), 1.5f, 1, 1);

        powersDic = new Dictionary<string, Power>();
        powersDic.Add("white", white);      // white
        powersDic.Add("cyan", cyan);        // cyan
        powersDic.Add("magenta", magenta);  // magenta
        powersDic.Add("yellow", yellow);    // yellow
        powersDic.Add("blue", blue);        // blue
        powersDic.Add("green", green);      // green
        powersDic.Add("red", red);          // red
        powersDic.Add("black", black);      // black

        // Initialize active powers' List
        activePowers = new LinkedList<string>();
        activePowers.AddFirst("white");
    }

    // Update is called once per frame
    void Update () {
        // Show Raycast on screen (just for debugging purposes)
        if ( controller.isFacingRight() )
            direction = 1;
        else
            direction = -1;
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + (direction*absortionDistance), transform.position.y), Color.green);

        // If player is in absortion range...
        if ( Physics2D.OverlapCircle(transform.position, absortionDistance, whatIsTarget) )
        {
            //... check if the player has the merging button pressed and if so...
            if (Input.GetButton("Merge"))
            {
                //... enable absorbtion
                absorb.setAbsorbing(true);
                // Find clone's GameObject
                player = Physics2D.OverlapCircle(transform.position, absortionDistance, whatIsTarget).gameObject;
                // Attract the player 
                absorb.absorbTarget(player);

                //Get color and atributtes from other player 
            }
        }
        
        // If merging button is released, cancel absorb
        if( Input.GetButtonUp("Merge"))
        {
            absorb.setAbsorbing(false);
        }
	}
}
