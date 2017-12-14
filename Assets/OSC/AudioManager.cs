using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient<int>("SuperCollider", "/start", 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
