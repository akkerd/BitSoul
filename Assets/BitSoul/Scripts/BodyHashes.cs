using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHashes : MonoBehaviour {

    public List<GameObject> bodyObjects;
    private int currentIndex;

    // Use this for initialization
    void Start ()
    {
        bodyObjects = new List<GameObject>();
        currentIndex = 0;
        bodyObjects.Add( GameObject.FindGameObjectWithTag("Player") );
        bodyObjects.Add( GameObject.Find("Magenta") );
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public GameObject GetNextCharacter()
    {
        if(bodyObjects.Count != 0 )
        {
            if (bodyObjects.Count-1 == currentIndex)
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

    public void addNewPlayer(GameObject p)
    {
        bodyObjects.Add(p);
    }
}
