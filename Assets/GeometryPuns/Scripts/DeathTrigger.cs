using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private LevelManager levelManager;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        GameObject player = other.gameObject;
        if (LayerMask.LayerToName(player.layer) == "Clone")
            levelManager.setCloneInactive(player);
        else
            levelManager.RespawnPlayer(player);

    }
}
