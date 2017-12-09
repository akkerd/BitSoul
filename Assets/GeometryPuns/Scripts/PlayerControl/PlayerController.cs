using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
    public int identifier;
    
    public Transform groundCheck;
	public float groundCheckRadius;   


	public LayerMask whatIsGround;

	public bool grounded;
    private bool faceRight;

    private Power activePower;



	protected Rigidbody2D player_rb;

    // Use this for initialization
    void Start()
    {
		InitPlayer();
    }

	public virtual void InitPlayer(){
		// This can be overwritten by inherited classes
		// for special abillities
		player_rb = GetComponent<Rigidbody2D>();
		faceRight = true;
	}

	public virtual void PlayerChecks(){
		// This can be overwritten by inherited classes
		// for special abillities
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

	}

    public virtual void Control(){
		// This can be overwritten by inherited classes
		// for special abillities
	}

	void FixedUpdate(){
		PlayerChecks();
	}

	void Update() {
		CheckJump();
		Control();
		CheckMove();
	}

    public bool isFacingRight()
    {
        return faceRight;
    }

	protected void CheckJump (){
		if(Input.GetButton("Jump") && grounded){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
		}
	}

	protected void CheckMove(){
/*		if(Input.GetKey(KeyCode.RightArrow)){
			player_rb.velocity = new Vector2(moveSpeed, player_rb.velocity.y);
			if (!faceRight)
				faceRight = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			player_rb.velocity = new Vector2(-moveSpeed, player_rb.velocity.y);
			if (faceRight)
				faceRight = false;
		}*/

		float horizontal = Input.GetAxis("Horizontal");

		if(horizontal != 0){
			player_rb.velocity = new Vector2(moveSpeed * horizontal, player_rb.velocity.y);
			if (horizontal < 0)
				faceRight = false;
			else{
				faceRight = true;
			}
		}
		//Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
	}

    public Color getActiveColor()
    {
        return activePower.getColor();
    }

    public void setActivePower( Power p)
    {
        activePower = p;
        moveSpeed = p.getSpeed();
        jumpSpeed = p.getJump();
        player_rb.mass = p.getWeight();
    }
}
