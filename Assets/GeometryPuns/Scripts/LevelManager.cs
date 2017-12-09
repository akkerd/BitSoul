using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	public GameObject deathParticle;

    //private PlayerController player;
    //private Color particleColor;
    private BodyManager bodyManager;

	public float respawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
        bodyManager = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<BodyManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}

	public void RespawnPlayer( GameObject playerToRespawn ){
        bodyManager.SwitchToIndex(0);
		StartCoroutine("RespawnPlayerCo", playerToRespawn);
	}

	private IEnumerator RespawnPlayerCo ( GameObject player ){
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.enabled = false;
        instantiateDeathParticles(player.transform, player.GetComponent<SpriteRenderer>().color);
		player.GetComponent<PlayerController>().enabled = false;
		player.GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(respawnDelay);
		pc.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
		player.transform.position = currentCheckpoint.transform.position;
	}

    public void setCloneInactive(GameObject cloneToSet)
    {
        instantiateDeathParticles(cloneToSet.transform, cloneToSet.GetComponent<SpriteRenderer>().color);
        cloneToSet.SetActive(false);

        int id = cloneToSet.GetComponent<PlayerController>().identifier;
        bodyManager.SetInactiveBody(id);
        resetFountain(id);
        bodyManager.SwitchToIndex(0);
    }

    private void resetFountain(int colorId)
    {
        GameObject[] fountains = GameObject.FindGameObjectsWithTag("ColorFountain");

        foreach (GameObject f in fountains)
        {
            CollectColor cc = f.GetComponent<CollectColor>();
            
            int id = cc.fountainColorCode;

            if (id == colorId)
            {
                cc.setFountainActive();
            }
        }
    }

    private void instantiateDeathParticles( Transform playerTr, Color playerColor )
    {
        ParticleSystem.MainModule system = deathParticle.GetComponent<ParticleSystem>().main;
        system.startColor = playerColor;
        Instantiate(deathParticle, playerTr.position, playerTr.rotation);
    }
}
