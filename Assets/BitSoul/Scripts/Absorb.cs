using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {


    private bool absorbing;
    private List<GameObject> targets;
    private BodyMixer mixer;

    // Use this for initialization
    void Start () {
        absorbing = false;
        targets = new List<GameObject>();
        mixer = GetComponentInParent<BodyMixer>();
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
            //if (other.gameObject.layer == LayerMask.GetMask("Clone") )
            {
                int otherID = other.gameObject.GetComponent<PlayerController>().identifier;
                Destroy(other.gameObject);

                mixer.takePowers(otherID);
            }
        }
    }
}
