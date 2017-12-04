using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {


    private bool absorbing;
    private List<GameObject> targets;
    private BodyMixer mixer;
    private BodyManager bodyManager;

    // Use this for initialization
    void Start () {
        absorbing = false;
        targets = new List<GameObject>();
        mixer = GetComponentInParent<BodyMixer>();
        bodyManager = FindObjectOfType<BodyManager>();

        if (bodyManager == null)
        {
            Debug.Log("Body Manger is not found");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void setAbsorbing(bool b)
    {
        absorbing = b;
    }

    private void OnTriggerStay2D( Collider2D other )
    {
        if (absorbing)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Clone" )
            {
                absorb(other.gameObject);
            }
        }
    }

    private void absorb(GameObject newPlayer)
    {
        int otherID = newPlayer.GetComponent<PlayerController>().identifier;

        // Save Clone GameObject in BodyManager
        int arrayIndex = mixer.calculateArrayIndex(otherID);
        bodyManager.StoreClone(arrayIndex);
        bodyManager.setActiveBody(arrayIndex, false);

        // Destroy Clone object
        //Destroy(newPlayer);

        // Disable Clone Gameobject
        newPlayer.SetActive(false);
        mixer.takeColor(otherID);
    }
}
