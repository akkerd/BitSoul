using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitting : MonoBehaviour {

    private BodyManager bm;


    private GameObject playable;

	// Use this for initialization
	void Start () {
        bm = FindObjectOfType<BodyManager>();
        playable = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        int index = -1;
        if (Input.GetButtonDown("White"))
            index = 0;
        else if (Input.GetButtonDown("Cyan"))
            index = 1;
        else if (Input.GetButtonDown("Magenta"))
            index = 2;
        else if (Input.GetButtonDown("Yellow"))
            index = 3;

        if (index > -1)
        {
            Debug.Log(LayerMask.LayerToName(playable.layer));
            if (LayerMask.LayerToName(playable.layer) != "Clone")
            {
                bm.SetActiveBody(index);
            }
            else
            {
                bm.SwitchToIndex(index);
            }

            playable = GameObject.FindGameObjectWithTag("Player");

        }
	}
}
