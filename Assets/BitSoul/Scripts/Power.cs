using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power {

    private int value; // Color number (using prime product)
    private Color color;
    private float jump;
    private float size;
    private float weight;

    public Power( int value, Color color, float jump, float size, float weight)
    {
        this.value = value;
        this.color = color;
        this.jump = jump;
        this.size = size;
        this.weight = weight;
    }
}
