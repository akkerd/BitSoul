using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {


    private bool absorbing;
    private List<GameObject> targets;
    private BodyMixer mixer;
    private BodyHashes bodyHashes;

    // Use this for initialization
    void Start () {
        absorbing = false;
        targets = new List<GameObject>();
        mixer = GetComponentInParent<BodyMixer>();
        bodyHashes = FindObjectOfType<BodyHashes>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void setAbsorbing(bool b)
    {
        absorbing = b;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (absorbing)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Clone" )
            {
                GameObject player = other.gameObject;
                int otherID = player.GetComponent<PlayerController>().identifier;
                // Save Clone GameObject in BodyHashes
                bodyHashes.addNewPlayer(player);
                // Destroy Clone object
                //Destroy(player);

                // Disable Clone Gameobject
                player.SetActive(false);
                mixer.takePowers(otherID);
            }
        }
    }
}
