using UnityEngine;
using System.Collections;

public class PlatformerController : MonoBehaviour {
	public AnimationClip idleAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;

	public float maxRunSpeed = 3.75f;
	private Vector3 runSpeed = new Vector3(0.0f,0.0f,0.0f);

	public float jumpHeight = 0.5f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private float vertical = 0.0f;
	private float horizontal = 0.0f;

	public bool climbing = false;
	CharacterController controller;
	Transform _transform;

	private Vector3 moveDirection;
	private Vector3 rotateDirection = Vector3.zero;

	public CharacterState _characterState;

	bool onGround = true;

	public enum CharacterState{
		Idle = 0,
		Running = 1,
		Jumping = 2
	};
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		_transform = GetComponent<Transform> ();
		moveDirection = new Vector3(0, -gravity, 0);
	}
	
	void Update() {

		
		vertical = Input.GetAxis ("Vertical");
		//Debug.Log ("vertical: "+ vertical *Time.deltaTime);
		if(vertical > 0){
			runSpeed.x += vertical * Time.deltaTime*1.5f;
		}
		else if(vertical <0){
			runSpeed.x -= -vertical * Time.deltaTime*1.5f;
		}
		else if(vertical == 0){
			if(runSpeed.x > 0)
				runSpeed.x -= Time.deltaTime*1.5f;
			else if(runSpeed.x < 0)
				runSpeed.x += Time.deltaTime*1.5f;
		}

		horizontal = Input.GetAxis ("Horizontal");
		if(horizontal > 0){
			runSpeed.z += horizontal * Time.deltaTime*1.5f;
		}
		else if(horizontal <0){
			runSpeed.z -= horizontal * Time.deltaTime*1.5f;
		}
		else if(horizontal == 0){
			if(runSpeed.z > 0)
				runSpeed.z -= Time.deltaTime*1.5f;
			else if(runSpeed.z < 0)
				runSpeed.z += Time.deltaTime*1.5f;
		}

		runSpeed = Vector3.ClampMagnitude (runSpeed, maxRunSpeed);

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
			if (onGround) {
				/*float horizontal = Input.GetAxis ("Horizontal");
				Debug.Log ("runSpeed:" + runSpeed);
				if(runSpeed < 0){
					moveDirection = new Vector3 (0, 0, -runSpeed*Time.deltaTime*100.0f);
				}
				else{
					moveDirection = new Vector3 (0, 0, runSpeed*Time.deltaTime*100.0f);
				}
				rotateDirection = new Vector3(0,horizontal*Time.deltaTime*100.0f,0);
				*/
				//rotate
				transform.LookAt(new Vector3(moveDirection.x, transform.position.y, moveDirection.z));
				/*Vector3 relative = transform.InverseTransformPoint (transform.position + moveDirection);
				float angle = Mathf.Atan2 (relative.x, relative.z) * Mathf.Rad2Deg;
				transform.Rotate (0, 0, -angle);*/
				//transform.Rotate(rotateDirection);

				moveDirection.x = runSpeed.x;
				moveDirection.z = runSpeed.z;
				//print (moveDirection);
				if (Input.GetButton ("Jump"))
					moveDirection.y = jumpSpeed;
			}
			transform.Translate(runSpeed);
		}
	}
}
