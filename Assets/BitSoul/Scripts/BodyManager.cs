using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour{

    private BodyHashes bh;
    private GameObject currentBody;
    private CameraManager cm;

    // Use this for initialization
    void Start()
    {
        bh = this.GetComponent<BodyHashes>();
        cm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchToNext()
    {
        /* Disable old body scripts and tags */
        currentBody.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().enabled = false;  
        currentBody.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
        currentBody.tag = "Untagged";
        
        currentBody = bh.GetNextCharacter();                                                            // Change to next body
        cm.changeTarget(currentBody.transform);                                                         // Change Camera Target

        /* Enable new body scripts and tags */
        currentBody.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().enabled = true;
        currentBody.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = true;
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
}
