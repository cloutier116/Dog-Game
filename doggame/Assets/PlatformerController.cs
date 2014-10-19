using UnityEngine;
using System.Collections;

public class PlatformerController : MonoBehaviour {
	public AnimationClip idleAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;

	public float runSpeed = 7.75f;
	public float jumpHeight = 0.5f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	public bool climbing = false;
	CharacterController controller;
	Transform _transform;

	private Vector3 moveDirection = Vector3.zero;

	public CharacterState _characterState;

	public enum CharacterState{
		Idle = 0,
		Running = 1,
		Jumping = 2
	};
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		_transform = GetComponent<Transform> ();
	}
	
	void Update() {
		if (Input.GetKeyDown (KeyCode.C)) { //enable climbing
			Collider[] hitColliders = Physics.OverlapSphere (_transform.position, 10.0f);
			int i = 0;
			while (i < hitColliders.Length) {
				//hitColliders[i].SendMessage("AddDamage");
				if (hitColliders [i].tag == "Ladder") {
					climbing = true;
				}
				i++;
			}
		}
		if (Input.GetKeyUp (KeyCode.C)) { //disable climbing
			climbing = false;
		}
	}

	void FixedUpdate(){
		
		if(climbing){
				
				moveDirection.y += gravity * Time.deltaTime;
				controller.Move (moveDirection * Time.deltaTime);
		}
		else{
			if (controller.isGrounded) {
				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= runSpeed;
				if (Input.GetButton ("Jump"))
					moveDirection.y = jumpSpeed;
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
		}
	}
}
