using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

    public float attractionForce;

    private bool absorbing;
    private GameObject target;
    private BodyMixer mixer;
    private float heading;
    private float direction;

    // Use this for initialization
    void Start () {
        absorbing = false;
        target = null;
        mixer = GetComponentInParent<BodyMixer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( absorbing )
            if( other.gameObject == target)
            {
                Destroy(target);
                int powerValue = -1;
                int.TryParse(other.gameObject.name, out powerValue);
                if (powerValue == -1)
                    Debug.Log("Power value is not a number");
                else
                    mixer.takePowers(powerValue);
            }
    }

    public void absorbTarget( GameObject other )
    {
        if( target != other )
            target = other;

        heading = ( GameObject.FindGameObjectWithTag("Player").transform.position - other.transform.position ).x;
        if (heading > 0)
            direction = 1;
        else
            direction = -1;

        other.GetComponent<Rigidbody2D>().AddForce(direction * transform.right * attractionForce);
    }

    public void setAbsorbing(bool b)
    {
        absorbing = b;
    }
}
