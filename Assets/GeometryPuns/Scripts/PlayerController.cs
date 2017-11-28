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
    private bool faceRight;

	private bool doubleJumped;
    private Power activePower;

	private Rigidbody2D player_rb;


    // Use this for initialization
    void Start () {
		player_rb = GetComponent<Rigidbody2D>();
        faceRight = true;
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
            if (!faceRight)
                faceRight = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			player_rb.velocity = new Vector2(-moveSpeed, player_rb.velocity.y);
            if (faceRight)
                faceRight = false;
        }
	}

    public bool isFacingRight()
    {
        return faceRight;
    }

    public Color getActiveColor()
    {
        return activePower.getColor();
    }

    public void setActivePower( Power p)
    {
        activePower = p;
    }
}
