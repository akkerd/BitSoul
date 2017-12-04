using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public bool xAxis;
    public float length;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(xAxis)
        {
            transform.position = new Vector2(Mathf.PingPong(Time.time, length), transform.position.y);
        } else
        {
            transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time, length));
        }
        
    }
}
