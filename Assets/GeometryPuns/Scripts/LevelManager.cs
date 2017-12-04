using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	public GameObject deathParticle;

	private PlayerController player;
    private Color particleColor;

	public float respawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
        //player = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RespawnPlayer(GameObject playerToRespawn){
        player = playerToRespawn.GetComponent<PlayerController>();
        particleColor = player.GetComponent<SpriteRenderer>().color;
		StartCoroutine("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo (){
        ParticleSystem.MainModule system = deathParticle.GetComponent<ParticleSystem>().main;
        system.startColor = particleColor;
		Instantiate(deathParticle, player.transform.position, player.transform.rotation);
		player.enabled = false;
		player.GetComponent<Renderer>().enabled = false;
		Debug.Log("PLayer respawn");
		yield return new WaitForSeconds(respawnDelay);
		player.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
		player.transform.position = currentCheckpoint.transform.position;

	}

}
