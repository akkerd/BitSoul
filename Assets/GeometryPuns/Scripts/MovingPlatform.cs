using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public bool xAxis;
    public float length;
    public float speed;
    private float baseY;
	// Use this for initialization
	void Start () {
        baseY = transform.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        if(xAxis)
        {
            transform.position = new Vector2(Mathf.PingPong(Time.time, length), transform.position.y);
        } else
        {
            Vector2 newPosition = new Vector2(transform.position.x, Mathf.PingPong(Time.time*speed, length) + baseY);
            //
            //Debug.Log("New position: "+ newPosition);
            //Debug.Log("PingPong: "+ Mathf.PingPong(Time.time, length));

            transform.position = newPosition;
        }
        
    }

    private GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject; 
        if (LayerMask.LayerToName(player.layer) == "Player" || LayerMask.LayerToName(player.layer) == "Clone")
        {
            player.transform.parent = this.transform;
            Debug.Log("Player"+ player.name +"is child");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player = other.gameObject;
        if (LayerMask.LayerToName(player.layer) == "Player" || LayerMask.LayerToName(player.layer) == "Clone")
        {
            player.transform.parent = null;
            Debug.Log("Player detached");

        }
    }
}
