using UnityEngine;
using System.Collections;

public class PlatformerController : MonoBehaviour {
	public AnimationClip idleAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;

	public float maxRunSpeed = 3.75f;
	private float verticalRunSpeed = 0.0f;
	private float horizontalRunSpeed = 0.0f;

	public float jumpHeight = 0.5f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private float vertical = 0.0f;
	private float horizontal = 0.0f;

	public bool climbing = false;
	CharacterController controller;
	Transform _transform;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotateDirection = Vector3.zero;

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
		vertical = Input.GetAxis ("Vertical");
		Debug.Log ("vertical: "+ vertical *Time.deltaTime);
		if(vertical > 0){
			verticalRunSpeed += Time.deltaTime*1.5f;
		}
		else if(vertical <0){
			verticalRunSpeed -= Time.deltaTime*1.5f;
		}
		else if(vertical == 0){
			verticalRunSpeed *= 0.5f;
		}
		if(verticalRunSpeed > maxRunSpeed || verticalRunSpeed < -maxRunSpeed){
			verticalRunSpeed = maxRunSpeed;
		}

		horizontal = Input.GetAxis ("Horizontal");
		Debug.Log ("horizontal: "+ horizontal *Time.deltaTime);
		if(horizontal > 0){
			horizontalRunSpeed += Time.deltaTime*1.5f;
		}
		else if(horizontal <0){
			horizontalRunSpeed -= Time.deltaTime*1.5f;
		}
		else if(horizontal == 0){
			horizontalRunSpeed *= 0.5f;
		}
		if(horizontalRunSpeed > maxRunSpeed || horizontalRunSpeed < -maxRunSpeed){
			horizontalRunSpeed = maxRunSpeed;
		}

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
				float rotation = Input.GetAxis ("PlayerRotation");
				Debug.Log ("verticalRunSpeed:" + verticalRunSpeed);
				Debug.Log ("horizontalRunSpeed:" + horizontalRunSpeed);
				
				int verticalModifier = 1;
				if(verticalRunSpeed < 0){
					verticalModifier = -1;
				}
				else if(verticalRunSpeed >0){
					verticalModifier = 1;
				}
				
				int horizontalModifier = 1;
				if(horizontalRunSpeed < 0){
					horizontalModifier = -1;
				}
				else if(horizontalRunSpeed >0){
					horizontalModifier = 1;
				}

				moveDirection = new Vector3 (horizontalModifier*horizontalRunSpeed*Time.deltaTime*100.0f, 0, verticalModifier*verticalRunSpeed*Time.deltaTime*100.0f);
				

				rotateDirection = new Vector3(0,rotation*Time.deltaTime*100.0f,0);
				
				//rotate
				transform.Rotate(rotateDirection);

				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= (verticalRunSpeed + horizontalRunSpeed);
				if (Input.GetButton ("Jump"))
					moveDirection.y = jumpSpeed;
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
		}
	}
}
