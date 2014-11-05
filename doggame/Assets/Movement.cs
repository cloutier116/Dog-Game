using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 velocity = new Vector3(0,0,0);
	public Quaternion fixedRotation = Quaternion.Euler(0f,0f,0f);
	public float accel = .2f;
	public bool[] directions = new bool[] {false, false, false, false};
									//     forward, back, left, right
	public float maxVel = 10f;


	public float hangFactor = 1.5f;

	public float topY;

	public float jumpForce = 300f;
	public float jumpingTime = 3.0f;

	public bool climbing = false;
	public float tsb = 0.0f;
	public Transform tr;
	public GameObject camera;
	public Transform camera_transform;
	public bool jump = false;
	
	public float heldDown = 0.0f;

	public bool onGround;
	public bool jumpUp;
	bool pressed = false;
	Vector3 cameraDirection;



	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		camera_transform = camera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		getInput ();
	}

	void FixedUpdate(){
		if(climbing){
			tr.rotation = fixedRotation;
			tr.rigidbody.useGravity = false;
			velocity = new Vector3(0f, 20f, 0f);
			velocity = Vector3.ClampMagnitude (velocity, maxVel);
			//tr.LookAt (tr.position + velocity);
			tr.Translate (0f, velocity.y, 0f);
		}
		else{

			float fdt = Time.fixedDeltaTime;

			tr.rigidbody.useGravity = true;
			if(directions[0])
				velocity.z += accel;
			else if(directions[1]){
				velocity.z -= accel;
				directions[1] = false;
			}
			else
			{
				if(velocity.z < accel/2 && velocity.z > -accel/2)
					velocity.z = 0;
				else if(velocity.z > 0)
					velocity.z -= accel;
				else if(velocity.z < 0)
					velocity.z += accel;
			}
			if(directions[2])
				velocity.x -= accel;
			else if(directions[3])
				velocity.x += accel;
			else
			{
				if(velocity.x < accel/2 && velocity.x > -accel/2)
					velocity.x = 0;
				else if(velocity.x > 0)
					velocity.x -= accel;
				else if(velocity.x < 0)
					velocity.x += accel;
			}
			velocity = Vector3.ClampMagnitude (velocity, maxVel);

			

			Vector3 forward;
			if (cameraDirection == Vector3.zero)
				forward = camera_transform.TransformDirection(Vector3.forward);
			else
				forward = cameraDirection;
			forward.y = 0f;
			forward = forward.normalized;
			Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
			
			Vector3 upward = new Vector3(0.0f,1.0f,0.0f);
			

			if(jumpUp && rigidbody.velocity.y > 0){
				jump = false;
				jumpUp =false;
				Debug.Log("Jump up");
				GameObject.Find("Main Camera").GetComponent<CameraController>().frozen = false;
				rigidbody.velocity = new Vector3(0,0,0);
				rigidbody.angularVelocity = Vector3.zero;
			}

			if(jump && onGround && !jumpUp){
				//Debug.Log("JUMPING!!");
				jump = false;
				GameObject.Find("Main Camera").GetComponent<CameraController>().frozen = true;
				onGround = false;
				//currentJumpSpeed = 3.0f;
				rigidbody.AddForce(Vector3.up * jumpForce);
				//rigidbody.velocity = new Vector3(0,0.1f,0);
			}
			
			
			
			
	

			Vector3 walkDirection = (velocity.x * right + velocity.z * forward);
			if(walkDirection != Vector3.zero){
				if(heldDown > 0.2f){
					tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(walkDirection), .15f);
				}
				else{
					tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(walkDirection), .05f);
				}
			}

			rigidbody.MovePosition(tr.position + walkDirection);
			//tr.position = tr.position + walkDirection;
			//tr.Translate (velocity.magnitude * camera_transform.forward);


		}
		
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.contacts.Length > 0)
		{
			foreach(ContactPoint contact in other.contacts)
			{
				if(onGround == false && !(Vector3.Dot(contact.normal, Vector3.up) > 0.5)){
					rigidbody.AddForceAtPosition(new Vector3(10,10,10), contact.point);
				}
			}
		}
	}
	void OnCollisionStay(Collision collision){
		onGround = false;
		if(collision.contacts.Length >0){
			foreach(ContactPoint contact in collision.contacts){//ContactPoint contact = collision.contacts[0];
				if(Vector3.Dot(contact.normal, Vector3.up) > 0.5){
					onGround = true;
					
					
				}
				if(onGround == false && !(Vector3.Dot(contact.normal, Vector3.up) > 0.5)){
					Vector3 force = new Vector3( 0, -25, 0);
					rigidbody.AddForceAtPosition(force, contact.point);
				}
			}
		}
	}

	void OnCollisionExit(Collision collision) {
		onGround = true;
		if(collision.contacts.Length >0){
			foreach(ContactPoint contact in collision.contacts){//ContactPoint contact = collision.contacts[0];
				if(Vector3.Dot(contact.normal, Vector3.up) > 0.5){
					onGround = false;
					
					
				}
			}
		}
	}

	void getInput()
	{

		
		tsb += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.C)) { //enable climbing
			fixedRotation = Quaternion.Euler(tr.rotation.x, tr.rotation.y, tr.rotation.z);
			Debug.Log ("C Down");
			Collider[] hitColliders = Physics.OverlapSphere (tr.position, 10.0f);
			int i = 0;
			while (i < hitColliders.Length) {
				//hitColliders[i].SendMessage("AddDamage");
				if (hitColliders [i].tag == "Ladder") {
					Debug.Log ("Ladder detected");
					velocity = new Vector3(0f,0f,0f);
					climbing = true;
				}
				i++;
			}
			velocity = new Vector3(0f,0f,0f);
			Debug.Log ("C Up");
			climbing = false;
		}

		if(Input.GetButton("Jump") && !pressed && onGround){
			
			pressed = true;
			print ("jump");
			onGround = false;
			jump = true;
			
		}
		else if (!Input.GetButton("Jump") && pressed){
			jumpUp = true;
			pressed = false;
			//print ("Cut jump short");
		}
		else if (!Input.GetButton("Jump") && onGround){
			pressed = false;
			//print ("landed");
		}

		for(int i = 0; i < directions.Length; i++)
			directions[i] = false;
		cameraDirection = Vector3.zero;

		if (Input.GetAxis ("Vertical") > 0)
			directions [0] = true;
		else if(Input.GetAxis("Vertical") < 0 && tsb > 1.0f)
		{
			tsb = 0.0f;
			directions [1] = true;
			//if(velocity.x == 0)
			//	cameraDirection = camera_transform.TransformDirection(Vector3.forward);
		}
		if (Input.GetAxis ("Horizontal") < 0 && directions[2] == true){
			directions [2] = true;
			heldDown += Time.deltaTime;
		}
		else if(Input.GetAxis ("Horizontal") < 0){
			directions [2] = true;
		}
		else if (Input.GetAxis ("Horizontal") > 0 && directions[3] == true){
			directions [3] = true;
			heldDown += Time.deltaTime;
		}
		else if(Input.GetAxis ("Horizontal") > 0){
			directions[3] = true;
		}
		else{
			heldDown = 0.0f;
		}

		
		if(Input.GetButtonDown("Bark"))
		   print("bark");

		return;
	}
}
