using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagentaController : PlayerController{

	public override void InitPlayer(){
		base.InitPlayer();
		base.moveSpeed = 3.5f;
		base.jumpSpeed = 7.5f;
	}

	public override void PlayerChecks(){
		base.PlayerChecks();
	}

	public override void Control(){
		base.Control();	
	}

}
