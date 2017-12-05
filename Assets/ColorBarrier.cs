using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour {

    public string color = "white";
    private Color wallColor;

    SpriteRenderer spriteRenderer;


    void Start () {
        color = color.ToLower();
        switch (color)
        {
            case "cyan":
                wallColor = Color.cyan;
                break;
            case "magenta":
                wallColor = Color.magenta;
                break;
            case "yellow":
                wallColor = Color.yellow;
                break;
            case "blue":
                wallColor = Color.blue;
                break;
            case "green":
                wallColor = Color.green;
                break;
            case "red":
                wallColor = Color.red;
                break;
            case "black":
                wallColor = Color.black;
                break;
            default:
                wallColor = Color.white;
                break;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = wallColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<SpriteRenderer>().color.Equals(wallColor))
            //if (Color.magenta.Equals(wallColor))
            {
                Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
            }
            else
            {
                Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), false);
            }
        }
    }
}
