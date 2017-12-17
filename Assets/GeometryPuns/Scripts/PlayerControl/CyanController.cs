using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanController : PlayerController{

	public float wallJumpSpeed = 20.0f;
	public Transform rightCheck;
	public Transform leftCheck;
	private float wallCheckRadius = 0.4f;
	public LayerMask whatIsWall;
	protected bool Lwalled;
	protected bool Rwalled;

	public bool doubleJumped;

	public override void InitPlayer(){
		base.InitPlayer();
	}

	public override void PlayerChecks(){
		base.PlayerChecks();
		Rwalled = Physics2D.OverlapCircle(rightCheck.position, wallCheckRadius, whatIsWall);
		Lwalled = Physics2D.OverlapCircle(leftCheck.position, wallCheckRadius, whatIsWall);
	}

	public override void Control(){
		if(grounded){
			doubleJumped = false;
		}
		CheckDoubleJump();
		base.Control();	
	}

	private void CheckDoubleJump(){
		if(Input.GetButtonDown("Jump") && !doubleJumped && !grounded && !(Rwalled || Lwalled)){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
			doubleJumped = true;
		}
		if(Input.GetButtonDown("Jump") && (Rwalled || Lwalled) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))){
			if(Rwalled){
				//player_rb.AddForce(new Vector2(-jumpSpeed * 200, jumpSpeed * 50));
				player_rb.velocity = new Vector2(-moveSpeed, wallJumpSpeed);
			}
			else{
				//player_rb.AddForce(new Vector2(jumpSpeed * 200, jumpSpeed * 50));
				player_rb.velocity = new Vector2(moveSpeed, wallJumpSpeed);
			}
		}
	}

}
