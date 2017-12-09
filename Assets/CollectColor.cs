using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectColor : MonoBehaviour {
    public int fountainColorCode;
    public Sprite emptyFountain;
    public Sprite fullFountain;
    public float transitionSeconds;

    private GameObject player;
    private SpriteRenderer[] spriteRenderers;
    private BodyMixer mixer;
    private BodyManager bodyManager;
    private GameObject body;

    // Use this for initialization
    void Start () {
        //sr = GetComponent<SpriteRenderer>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        bodyManager = FindObjectOfType<BodyManager>();
        spriteRenderers[0].sprite = fullFountain;
        spriteRenderers[1].sprite = emptyFountain;
        spriteRenderers[1].enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player & not a clone
        if (gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            if (player.tag == "Player" && LayerMask.LayerToName(player.layer) != "Clone")
            {
                mixer = player.GetComponentInParent<BodyMixer>();
                mixer.takeColor(fountainColorCode);
                int arrIndex = mixer.calculateArrayIndex(fountainColorCode);
                bodyManager.StoreClone(arrIndex);

                setFountainInactive();
            }
        }

    }

    public void setFountainActive()
    {        
        StartCoroutine("RespawnPaint");
    }

    private IEnumerator RespawnPaint()
    {
        float _progress = 0.0f;
        spriteRenderers[0].enabled = true;
        while (_progress < 1)
        {
            Color _tmpColor = spriteRenderers[0].color;
            spriteRenderers[0].color = new Color(_tmpColor.r, _tmpColor.g, _tmpColor.b, _progress); //startAlpha = 0 <-- value is in tmp.a
            _progress += Time.deltaTime/transitionSeconds;
            yield return null;
        }

        Debug.Log("I'm being called");
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
       
        spriteRenderers[1].enabled = false;
    }

    public void setFountainInactive()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderers[0].enabled = false;
        spriteRenderers[1].enabled = true;
    }
   
    //TODO: Code to change sprite back when a clone dies. 
}
