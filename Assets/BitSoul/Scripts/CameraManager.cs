using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private UnityStandardAssets._2D.Camera2DFollow cam2d;

	// Use this for initialization
	void Start () {
        cam2d = this.GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeTarget( Transform newTarget )
    {
        cam2d.target = newTarget;
    }
}
