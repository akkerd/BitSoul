using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilePlatform : MonoBehaviour
{

    public LevelManager levelManager;
    public string color = "yellow";
    private Vector2 defaultPower = new Vector2(0, 15.0f);
    private Vector2 strongPower = new Vector2(0, 30.0f);
    private Vector2 power;
    private Color platformColor;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        power = defaultPower;
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
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (other.name == "Player" || LayerMask.LayerToName(player.layer) == "Clone")
        {
            Color playerColor = player.GetComponent<SpriteRenderer>().color;

            if (playerColor.Equals(platformColor))
            {
                if (LayerMask.LayerToName(player.layer) == "Clone")
                {
                    power = strongPower;
                }
                else
                {
                    power = defaultPower;
                }
                player.GetComponent<Rigidbody2D>().AddRelativeForce(power, ForceMode2D.Impulse);
            }
            else
            {
                levelManager.RespawnPlayer(player);
            }
        }
    }
}
