using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHashes : MonoBehaviour {

    public GameObject[] bodyObjects;
    private int currentIndex;
    private BodyManager bm;

    // Use this for initialization
    void Start ()
    {
        bm = this.GetComponent<BodyManager>();
        currentIndex = 0;
        bm.SetCurrentBody(bodyObjects[currentIndex]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public GameObject GetNextCharacter()
    {
        if(bodyObjects.Length != 0 )
        {
            if (bodyObjects.Length-1 == currentIndex)
                currentIndex = 0;
            else
                currentIndex++;

            return bodyObjects[currentIndex];
        }
        else
        {
            Debug.Log("Error: Character list is empty!");
        }

        return null;
    }

    public GameObject getCurrentBody()
    {
        return bodyObjects[currentIndex];
    }
}
