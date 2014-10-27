using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {



	public AnimationClip idleAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;

	public CharacterState _characterState;
	CharacterController controller;
	public enum CharacterState{
		Idle = 0,
		Running = 1,
		Jumping = 2,
		Climbing = 3
	};
	// Use this for initialization
	void Start () {
		//controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update() {
		

		
		if (Input.GetKeyDown (KeyCode.C)) { //enable climbing
			_characterState = CharacterState.Climbing;
		}
		if (Input.GetKeyUp (KeyCode.C)) { //disable climbing
			_characterState = CharacterState.Idle;
		}
	}
	
	void FixedUpdate(){
		
		if (_characterState == CharacterState.Climbing) 
		{
		}
		else{
			if(_characterState == CharacterState.Jumping)
			{
				animation.Play ("jump_pose");
			}
			else if (_characterState == CharacterState.Running)
			{
				animation.Play ("run");
			}
			else//idle
			{
				animation.Play ("idle");
			}
			if (true)//controller.isGrounded)
			{
				if (Input.GetButton ("Jump"))
					_characterState = CharacterState.Jumping;
				else if (Input.GetButton ("Horizontal") || Input.GetButton("Vertical"))
					_characterState = CharacterState.Running;
				else {
					_characterState = CharacterState.Idle;
				}
			}

		}
	}

}
