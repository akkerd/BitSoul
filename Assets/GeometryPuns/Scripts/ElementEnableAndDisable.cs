using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEnableAndDisable : MonoBehaviour {

    public List<GameObject> elements;
    public Color mainColor;
    private GameObject player;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = mainColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player 
        if (player.tag == "Player")
        {
            sr.color = Color.gray;
            foreach (GameObject element in elements)
            {
                Renderer[] renderers = element.GetComponents<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    renderer.GetComponent<Renderer>().enabled = true;
                }

                BoxCollider2D[] boxColliders = player.GetComponents<BoxCollider2D>();

                foreach (BoxCollider2D collider in boxColliders)
                {
                    collider.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        player = other.gameObject;

        if (player.tag == "Player")
        {
            sr.color = mainColor;
            foreach (GameObject element in elements)
            {
                Renderer[] renderers = element.GetComponents<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    renderer.GetComponent<Renderer>().enabled = false;
                }

                BoxCollider2D[] boxColliders = element.GetComponents<BoxCollider2D>();

                foreach (BoxCollider2D collider in boxColliders)
                {
                    collider.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}
