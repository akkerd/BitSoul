using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMixer : MonoBehaviour {
    
    private Vector3 forward;
    private bool wRay;
    private IDictionary<int, Color> colorDictonary;
    private LinkedList<int> activePowers;
    private PlayerController controller;
    
    private float direction;
    private bool playerInZone;

    private SpriteRenderer sprite;
    
    void Start () {
        // Find secondary classes
        controller = GetComponent<PlayerController>();

        colorDictonary = new Dictionary<int, Color>();
        colorDictonary.Add(1, Color.white); // white
        colorDictonary.Add(3, Color.cyan);        // cyan
        colorDictonary.Add(5, Color.magenta);  // magenta
        colorDictonary.Add(7, Color.yellow);    // yellow
        colorDictonary.Add(15, Color.blue);        // blue
        colorDictonary.Add(21, Color.green);      // green
        colorDictonary.Add(35, Color.red);          // red
        colorDictonary.Add(105, Color.black);     // black

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

        Debug.DrawLine(transform.position, new Vector2(transform.position.x + (direction*10), transform.position.y), Color.green);
	}

    public void takeColor(int colorID)
    {
        activePowers.AddLast(colorID);
        int mergedColor = 1;
        // Calculate new color value with our "prime numbers color calculation" system
        foreach (int i in activePowers)
        {
            mergedColor *= i;
        }

        // Set attributes of the new power on the player
        Debug.Log(mergedColor);
        sprite.color = colorDictonary[mergedColor];
    }

    public int calculateArrayIndex( int id )
    {
        return (id - 1) / 2;
    }
}
