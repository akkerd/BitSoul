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
    private IDictionary<int, Power> powersDic;
    private LinkedList<int> activePowers;
    private PlayerController controller;
    
    private float direction;
    private bool playerInZone;

    private SpriteRenderer sprite;
    
    void Start () {
        // Find secondary classes
        absorb = GetComponentInChildren<Absorb>();
        controller = GetComponent<PlayerController>();

        //Initialize powers' dictionary
        Power white =   new Power("white", Color.white, 1, 1, 1);
        Power cyan =    new Power("cyan", Color.cyan, 1.5f, 1, 1);
        Power magenta = new Power("magenta", Color.magenta, 0.5f, 1.5f, 1.5f);
        Power yellow =  new Power("yellow", Color.yellow, 1, 0.5f, 0.5f);
        Power blue =    new Power("blue", Color.blue, 1, 1.5f, 1.5f);
        Power green =   new Power("green", Color.green, 1.5f, 0.5f, 0.5f);
        Power red =     new Power("red", Color.red, 1, 1, 1);
        Power black =   new Power("black", Color.black, 1.5f, 1, 1);

        powersDic = new Dictionary<int, Power>();
        powersDic.Add(1, white);      // white
        powersDic.Add(3, cyan);        // cyan
        powersDic.Add(5, magenta);  // magenta
        powersDic.Add(7, yellow);    // yellow
        powersDic.Add(15, blue);        // blue
        powersDic.Add(21, green);      // green
        powersDic.Add(35, red);          // red
        powersDic.Add(105, black);      // black

        // Initialize active powers' List
        activePowers = new LinkedList<int>();
        activePowers.AddFirst(1);

        // Components to change when absorb
        sprite = GetComponent<SpriteRenderer>();
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
        if ( Physics2D.OverlapCircle(transform.position, absortionDistance, whatIsTarget ) )
        {
            //... check if the player has the merging button pressed and if so...
            if ( Input.GetKey( KeyCode.M ) )
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
        if( Input.GetButtonUp("Merge") )
        {
            absorb.setAbsorbing(false);
        }
	}

    public void takePowers(int power)
    {
        activePowers.AddLast(power);
        int powerIndex = 1;
        // Calculate new color value with our "prime numbers color calculation" system
        foreach (int i in activePowers)
        {
            powerIndex *= i;
        }

        // Set new power as the Active power
        Power newPower = powersDic[powerIndex];
        controller.setActivePower(newPower);

        // Set attributes of the new power on the player
        sprite.color = newPower.getColor();
    }

    /*public void takePowers(string power)
    {
        int powerValue = -1;
        // Search in the powers dictionary for the value of that color
        foreach ( int i in powersDic.Keys )
        {
            if (powersDic[i].getColorName() == power)
            {
                powerValue = i;
                break;
            }
        }
        // Security check, if power not found stop
        if( powerValue == -1)
        {
            return;
        }

        activePowers.AddLast(powerValue);
        int powerIndex = 1;
        // Calculate new color value with our "prime numbers color calculation" system
        foreach ( int i in activePowers)
        {
            powerIndex *= i;
        }

        // Set new power as the Active power
        Power newPower = powersDic[powerIndex];
        controller.setActivePower(newPower);

        // Set attributes of the new power on the player
        sprite.color = newPower.getColor();
    }*/
}
