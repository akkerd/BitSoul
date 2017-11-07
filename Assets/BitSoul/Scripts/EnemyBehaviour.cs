using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 2.5f;
    public List<GameObject> nodeList;
    public float marginOfError = 0.5f;
    public int damage = 1;

    private Vector2 direction;
    private int nodeNumber;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        nodeNumber = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        direction = new Vector2(nodeList[nodeNumber].transform.position.x - transform.position.x,
                                nodeList[nodeNumber].transform.position.y - transform.position.y);
        if (Math.Abs(direction.x) < marginOfError && Math.Abs(direction.y) < marginOfError)
        {
            nodeNumber = (nodeNumber + 1) % nodeList.Count;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.AddForce(direction.normalized * speed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage playerScript = collision.GetComponent<PlayerDamage>();
            playerScript.TakeDamage(this.damage);
        }
    }
}
