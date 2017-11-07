using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityFlip : MonoBehaviour {

    public Sprite red;
    public Sprite gray;

    private GameObject player;
    private bool flipping;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        flipping = false;
        sr = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if( !flipping )
        {
            flipping = true;
            player = other.gameObject;

            player.GetComponent<Rigidbody2D>().gravityScale *= -1;
            player.transform.Rotate(0f, 180f, 180f, Space.World);
            player.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().m_JumpForce *= -1;
            changeSprite();

            StartCoroutine( UnableFor(2f) );
        }

    }

    IEnumerator UnableFor( float sec )
    {
		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(sec);

        flipping = false;
        changeSprite();
		this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void changeSprite()
    {
        if( flipping )
        {
            sr.sprite = gray;
        }
        else
        {
            sr.sprite = red;
        }
    }
}
