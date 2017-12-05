using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
    public int identifier;
    public float wallJumpSpeed = 20.0f;
    public Transform groundCheck;
	public float groundCheckRadius;
    public LayerMask whatToIgnore;
    public Transform rightCheck;
	public Transform leftCheck;
	private float wallCheckRadius = 0.4f;
	public LayerMask whatIsGround;

	public bool grounded;
    private bool faceRight;
	public bool doubleJumped;
    private Power activePower;
	public LayerMask whatIsWall;
	protected bool Lwalled;
	protected bool Rwalled;
	private bool wallJumped = false;

	protected Rigidbody2D player_rb;

    // Use this for initialization
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        faceRight = true;
    }

    public virtual void Control(){
		// This can be overwritten by inherited classes
		// for special abillities
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		Rwalled = Physics2D.OverlapCircle(rightCheck.position, wallCheckRadius, whatIsWall);
		Lwalled = Physics2D.OverlapCircle(leftCheck.position, wallCheckRadius, whatIsWall);
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
		if(Input.GetKeyDown(KeyCode.Space) && grounded){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
		}
	}

	protected void CheckMove(){
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
