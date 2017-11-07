using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementReveal : MonoBehaviour {
    public GameObject exit;
    public Color mainColor;
    private GameObject player;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = mainColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player 
        if (player.tag == "Player")
        {
            sr.color = Color.gray;
            exit.GetComponent<Renderer>().enabled = true;
            exit.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        player = other.gameObject;

        if(player.tag == "Player")
        {
            sr.color = mainColor;
            exit.GetComponent<Renderer>().enabled = false;
            exit.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
