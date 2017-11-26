using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
        //player = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10.0f);
	}
}
