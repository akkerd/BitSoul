using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
        //player = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10.0f);
	}

    public void setCameraOnPlayer( GameObject p )
    {
        player = p;
    }
}
