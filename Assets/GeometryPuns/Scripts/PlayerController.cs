using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public float jumpSpeed = 15.0f;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJumped;

	private Rigidbody2D player_rb;


	// Use this for initialization
	void Start () {
		player_rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	void Update() {
		if(grounded){
			doubleJumped = false;
		}
		if(Input.GetKeyDown(KeyCode.Space) && grounded){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
		}
		if(Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
			doubleJumped = true;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			player_rb.velocity = new Vector2(moveSpeed, player_rb.velocity.y);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			player_rb.velocity = new Vector2(-moveSpeed, player_rb.velocity.y);
		}
	}
}
