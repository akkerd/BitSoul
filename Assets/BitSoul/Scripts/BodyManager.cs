using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour{

    private BodyHashes bh;
    private BodyMixer mixer;
    private GameObject[] bodyObjects;
    private bool[] activeBodies;
    private bool[] storedBodies;
    private GameObject currentBody;
    private int currentIndex;
    private CameraController camControl;

    public GameObject magenta;
    public GameObject cyan;
    public GameObject yellow;

    UIManager ui;

    // Use this for initialization
    void Start()
    {
        bh = GetComponent<BodyHashes>();

        // TODO get and assign GameObjects 
        bodyObjects = new GameObject[4];
        activeBodies = new bool[4] { true, false, false, false };
        storedBodies = new bool[4] { true, false, false, false};

        currentIndex = 0;
        // Find player and set it as first active Body
        currentBody = GameObject.FindGameObjectWithTag("Player");
        mixer = currentBody.GetComponent<BodyMixer>();

        cyan.SetActive(false);
        magenta.SetActive(false);
        yellow.SetActive(false);

        bodyObjects[0] = currentBody;
        bodyObjects[1] = cyan;
        bodyObjects[2] = magenta;
        bodyObjects[3] = yellow;

        ui = FindObjectOfType<UIManager>();

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
        int colorCode = mixer.calculateColorFromIndex(currentIndex);
        ui.uiSetInactive(colorCode);

        for ( int i = currentIndex+1; i < currentIndex+4; i++  )
        {
            int ind = i % 4;
            if (activeBodies[ind])
            {
                currentBody = bodyObjects[ind];
                currentIndex = ind;
                colorCode = mixer.calculateColorFromIndex(ind);
                ui.uiSetActive(colorCode);
                break;
            }
        }

       
        camControl.setCameraOnPlayer(currentBody);                          // Change Camera Target

        /* Enable new body scripts and tags */
        setPlayerComponents(currentBody, true);
        currentBody.tag = "Player";

        // For ignoring clone collision
        int currentIdentifier = currentBody.GetComponent<PlayerController>().identifier;
        if (currentIdentifier == 1)
            Physics2D.IgnoreLayerCollision(11, 9, true);
        else
            Physics2D.IgnoreLayerCollision(11, 9, false);

        // Sound events
        OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/player", currentIdentifier);
    }

    public void SwitchToIndex(int index)
    {
        if (currentIndex != index)
        { 
            if (activeBodies[index])
            {
                int colorCode = mixer.calculateColorFromIndex(currentIndex);
                ui.uiSetInactive(colorCode);
                setPlayerComponents(currentBody, false);
                currentBody.tag = "Untagged";

                currentBody = bodyObjects[index];
                currentIndex = index;
                colorCode = mixer.calculateColorFromIndex(index);
                ui.uiSetActive(colorCode);

                camControl.setCameraOnPlayer(currentBody);                          // Change Camera Target

                /* Enable new body scripts and tags */
                setPlayerComponents(currentBody, true);
                currentBody.tag = "Player";

                // For ignoring slone collisions
                if (index == 0)
                    Physics2D.IgnoreLayerCollision(11, 9, true);
                else
                    Physics2D.IgnoreLayerCollision(11, 9, false);

                // Sound events
                OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/player", currentBody.GetComponent<PlayerController>().identifier);
            }
            else
            {
                // Play wrong sound
                OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/wrongPlayer", 1);
            }
        }
    }

    public void SetCurrentBody( GameObject current)
    {
        currentBody = current;
    }

    public GameObject GetCurrentBody()
    {
        return currentBody;
    }

    public bool SetActiveBody(int index)
    {
        if (activeBodies[index])
        {
            SwitchToIndex(index);
            return true;
        }
        else if (storedBodies[index])
        {
            Vector3 variance = new Vector3(3, 0);
            /*if (GetComponent<PlayerController>().isFacingRight())
                variance = new Vector3(3, 0);
            else
                variance = new Vector3(-3, 0);
                */

            // Remove color from MainPLayer list in order to calculate the remaining color
            mixer.withdrawColor( (index*2)+1 );

            Vector3 newpos = currentBody.transform.position + variance;

            //Debug.Log(newpos + "  " + currentBody.transform.position + "  " + variance);
            bodyObjects[index].transform.SetPositionAndRotation(newpos, currentBody.transform.rotation);
            bodyObjects[index].SetActive(true);

            activeBodies[index] = true;
            SwitchToIndex(index);

            return true;
        }
        else
        {
            // Play wrong sound
            OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/wrongPlayer", 1);
        }

        return false;
    }

    public void SetInactiveBody(int identifier)
    {
        int index = mixer.calculateArrayIndex(identifier);
        activeBodies[index] = false;
        storedBodies[index] = false;
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

    public void StoreClone(int identifier)
    {
        storedBodies[identifier] = true;
    }

    public void setActiveBody(int identifier, bool b)
    {
        activeBodies[identifier] = b;
    }
}
