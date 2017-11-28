using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power{

    private string colorName; // Color number (using prime product)
    private Color color;
    private float jump;
    private float size;
    private float weight;

    public Power( string colorName, Color color, float jump, float size, float weight)
    {
        this.colorName = colorName;
        this.color = color;
        this.jump = jump;
        this.size = size;
        this.weight = weight;
    }
    public string getColorName()
    {
        return colorName;
    }

    public Color getColor()
    {
        return color;
    }
    
}
