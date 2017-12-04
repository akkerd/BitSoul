using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectColor : MonoBehaviour {
    public int fountainColorCode;
    public Sprite emptyFountain;
    public Sprite fullFountain;
    private GameObject player;
    private SpriteRenderer sr;
    private BodyMixer mixer;
    private BodyManager bodyManager;
    private GameObject body;

    // Use this for initialization
    void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        
        bodyManager = FindObjectOfType<BodyManager>();
        sr.sprite = fullFountain;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player & not a clone
        if (this.gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            if (player.tag == "Player" && LayerMask.LayerToName(player.layer) != "Clone")
            {
                mixer = player.GetComponentInParent<BodyMixer>();
                mixer.takeColor(fountainColorCode);
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                int arrIndex = mixer.calculateArrayIndex(fountainColorCode);
                bodyManager.StoreClone(arrIndex);
                sr.sprite = emptyFountain;
            }
        }

    }
    


    //TODO: Code to change sprite back when a clone dies. 
}
