using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour {

    //public LevelManager levelManager;
    private LevelManager levelManager;

    // Use this for initialization
    void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D ( Collider2D other){
		if(other.tag == "Player")
        {
            GameObject player = other.gameObject;
            if (LayerMask.LayerToName(player.layer) == "Clone")
                if (player.GetComponent<PlayerController>().identifier == 5)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    levelManager.RespawnPlayer(player);
                }
            else
            {
                levelManager.RespawnPlayer(player);

            }
		}
	}
}
