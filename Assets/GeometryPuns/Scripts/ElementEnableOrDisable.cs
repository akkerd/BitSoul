using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEnableOrDisable : MonoBehaviour {
    public bool enable;
    public List<GameObject> elements;
    public Color mainColor;
    private SpriteRenderer sr;
    private GameObject player;

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
                element.GetComponent<Renderer>().enabled = enable;
                element.GetComponent<BoxCollider2D>().enabled = enable;
            }

        }

    }
}
