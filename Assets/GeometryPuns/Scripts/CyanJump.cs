using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanJump : PlayerController{

	public override void Control(){
		if(grounded){
			doubleJumped = false;
		}
		CheckDoubleJump();
		base.Control();
		
	}

	private void CheckDoubleJump(){
		if(Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded && !(Rwalled || Lwalled)){
			player_rb.velocity = new Vector2(0.0f, jumpSpeed);
			doubleJumped = true;
		}
		if(Input.GetKeyDown(KeyCode.Space) && (Rwalled || Lwalled) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))){
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
