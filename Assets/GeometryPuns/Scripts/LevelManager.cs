using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	public GameObject deathParticle;
    public int framesTillHello;
    public float respawnDelay = 0.5f;

    private BodyManager bodyManager;
    UIManager ui;
    private int timestamp;
    private int previousSelector;
    private int r;
    private string[] idleSounds;
    private bool respawning;
    private int speed;

    // Use this for initialization
    void Start () {
        bodyManager = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<BodyManager>();
        ui = FindObjectOfType<UIManager>();
        timestamp = 0;
        idleSounds = new string[] { "hello.wav", "hey.wav", "look.wav", "listen.wav" };
        respawning = false;
        speed = 4;
    }

    private void FixedUpdate()
    {
        if ( !Input.anyKey )
        {
            //Starts counting when no button is being pressed
            timestamp += 1;
        }
        else
        {
            // If a button is being pressed restart counter to Zero
            timestamp = 0;
        }

        if (timestamp == framesTillHello)
        {
            while (r == previousSelector)
            {
                r = Random.Range(0, 4);
            }

            previousSelector = r;
            timestamp = 0;

            OSCHandler.Instance.SendMessageToClient<string>("SuperCollider", "/sampler", idleSounds[r]);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/stop", 1);
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += 1;
            OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/changeSpeedST", speed);
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= 1;
            OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/changeSpeedST", speed);
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}

	public void LoadNext(){
		int scene = SceneManager.GetActiveScene().buildIndex;

		SceneManager.LoadScene(scene + 1);
	}

	public void RespawnPlayer( GameObject playerToRespawn ){
        bodyManager.SwitchToIndex(0);
		StartCoroutine("RespawnPlayerCo", playerToRespawn);
	}

	private IEnumerator RespawnPlayerCo ( GameObject player ){
        respawning = true;
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.enabled = false;
        instantiateDeathParticles(player.transform, player.GetComponent<SpriteRenderer>().color);
		player.GetComponent<PlayerController>().enabled = false;
		player.GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(respawnDelay);

        respawning = true;
        pc.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
		player.transform.position = currentCheckpoint.transform.position;
	}

    public void setCloneInactive(GameObject cloneToSet)
    {
        instantiateDeathParticles(cloneToSet.transform, cloneToSet.GetComponent<SpriteRenderer>().color);
        cloneToSet.SetActive(false);

        int id = cloneToSet.GetComponent<PlayerController>().identifier;
        ui.uiLoseColor(id);
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
