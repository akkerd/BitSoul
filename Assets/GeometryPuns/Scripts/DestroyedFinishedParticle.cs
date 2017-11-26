using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedFinishedParticle : MonoBehaviour {

	private ParticleSystem thisPaticleSystem;
	// Use this for initialization
	void Start () {
		thisPaticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(thisPaticleSystem.isStopped){
			Destroy(gameObject);
		}	
	}
}
