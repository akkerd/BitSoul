using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public LevelManager levelmanager;

	// Use this for initialization

	void OnTriggerEnter2D (Collider2D other){

		levelmanager.LoadNext();
	}
}
