using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour {

    public GameObject transportLink;

    private GameObject player;

    // Use this for initialization
    void Start () {
		
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
            player.transform.position = transportLink.transform.position;
        }

        StartCoroutine(Delay(2f));

    }


    IEnumerator Delay(float sec)
    {

        transportLink.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(sec);
        transportLink.gameObject.GetComponent<BoxCollider2D>().enabled = true;


    }
}
