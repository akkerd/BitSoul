using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour{

    private BodyHashes bh;
    private GameObject currentBody;
    private CameraController camControl;

    // Use this for initialization
    void Start()
    {
        bh = GetComponent<BodyHashes>();
        
        // Find player
        currentBody = GameObject.FindGameObjectWithTag("Player");

        camControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        //camControl.setCameraOnPlayer(currentBody);
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("NextBody"))
        {
            SwitchToNext();
        }
    }

    public void SwitchToNext()
    {
        /* Disable old body scripts and tags */
        setPlayerComponents(currentBody, false);
        currentBody.tag = "Untagged";
        
        currentBody = bh.GetNextCharacter();                                                            // Change to next body
        camControl.setCameraOnPlayer(currentBody);                                                      // Change Camera Target

        /* Enable new body scripts and tags */
        setPlayerComponents(currentBody, true);
        currentBody.tag = "Player";
    }

    public void SetCurrentBody( GameObject current)
    {
        currentBody = current;
    }

    public GameObject GetCurrentBody()
    {
        return currentBody;
    }

    MonoBehaviour[] comps;
    private void setPlayerComponents( GameObject go, bool isEnabled )
    {
        comps = go.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour c in comps)
        {
            c.enabled = isEnabled;
        }

        if( !isEnabled )
        {
            go.GetComponent<BoxCollider2D>().enabled = true;
            go.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
