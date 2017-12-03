using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power{

    private string colorName; // Color number (using prime product)
    private Color color;
    private float jump;
    private float size;
    private float weight;
    private float speed;

    public Power( string colorName, Color color, float jump, float size, float weight, float speed)
    {
        this.colorName = colorName;
        this.color = color;
        this.jump = jump;
        this.size = size;
        this.weight = weight;
        this.speed = speed;
    }
    public string getColorName()
    {
        return colorName;
    }

    public Color getColor()
    {
        return color;
    }

    public float getJump()
    {
        return jump;
    }
    public float getSize()
    {
        return size;
    }

    public float getWeight()
    {
        return weight;
    }
    public float getSpeed()
    {
        return speed;
    }
}
