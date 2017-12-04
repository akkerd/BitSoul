using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHashes : MonoBehaviour {

    public List<GameObject> storedBodyObjects;
    private int currentIndex;

    // Use this for initialization
    void Start ()
    {
        storedBodyObjects = new List<GameObject>();
        currentIndex = 0;
        //storedBodyObjects.Add( GameObject.FindGameObjectWithTag("Player") );
        //bodyObjects.Add( GameObject.Find("Magenta") );
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public GameObject GetNextCharacter()
    {
        if(storedBodyObjects.Count != 0 )
        {
            if (storedBodyObjects.Count-1 == currentIndex)
                currentIndex = 0;
            else
                currentIndex++;

            return storedBodyObjects[currentIndex];
        }
        else
        {
            Debug.Log("Error: Character list is empty!");
        }

        return null;
    }

    public GameObject getCurrentBody()
    {
        return storedBodyObjects[currentIndex];
    }

    public void addNewPlayer(GameObject p)
    {
        storedBodyObjects.Add(p);
    }
}
