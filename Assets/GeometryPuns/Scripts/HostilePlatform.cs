using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilePlatform : MonoBehaviour {

	public LevelManager levelManager;
    public string color = "yellow";
    public Vector2 power = new Vector2(0,2.0f);
    private Color platformColor;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start () {
		levelManager = FindObjectOfType<LevelManager>();
        color = color.ToLower();
        switch (color)
        {
            case "cyan":
                platformColor = Color.cyan;
                break;
            case "magenta":
                platformColor = Color.magenta;
                break;
            case "yellow":
                platformColor = Color.yellow;
                break;
            case "blue":
                platformColor = Color.blue;
                break;
            case "green":
                platformColor = Color.green;
                break;
            case "red":
                platformColor = Color.red;
                break;
            case "black":
                platformColor = Color.black;
                break;
            default:
                platformColor = Color.white;
                break;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = platformColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D ( Collider2D other){
        if (other.name == "Player")
        {
            GameObject player = other.gameObject;
            Color playerColor = player.GetComponent<SpriteRenderer>().color;

            if ( playerColor.Equals(platformColor) )
            {
                player.GetComponent<Rigidbody2D>().AddRelativeForce(power, ForceMode2D.Impulse);
            }
            else
            {
                levelManager.RespawnPlayer(player);
            }
        }
    }
}
