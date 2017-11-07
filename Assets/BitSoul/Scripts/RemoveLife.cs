using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class RemoveLife : MonoBehaviour {


    public int damage = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlatformerCharacter2D characterScript = other.GetComponent<PlatformerCharacter2D>();
            PlayerDamage damageScript = other.GetComponent<PlayerDamage>();
            GameObject player = other.gameObject;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb.gravityScale != characterScript.startGravity)
            {
                rb.gravityScale = characterScript.startGravity;
                rb.velocity = Vector2.zero;
                player.transform.Rotate(0f, 180f, 180f, Space.World);
                characterScript.m_JumpForce *= -1;
            }
            damageScript.TakeDamage(damage);
            characterScript.transform.position = characterScript.startPosition;
        }
    }
}
