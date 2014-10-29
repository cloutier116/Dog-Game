﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 velocity = new Vector3(0,0,0);
	public Quaternion fixedRotation = Quaternion.Euler(0f,0f,0f);
	public float accel = .5f;
	public bool[] directions = new bool[] {false, false, false, false};
									//     forward, back, left, right
	public float maxVel = 10f;

	public float jumpHeldDownFor = 0.0f;

	public float defaultJumpSpeed = 5.0f;

	public float hangFactor = 1.5f;
	public float jumpIncrease = 30.0f; 

	public float topY;

	public bool climbing;
	public float tsb = 0.0f;
	public Transform tr;
	public GameObject camera;
	public Transform camera_transform;
	public bool jump = false;

	public float currentJumpSpeed = 0f;


	public float jumpSpeed = 5f;

	public bool onGround;
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
				if(velocity.z < accel && velocity.z > -accel)
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
				if(velocity.x < accel && velocity.x > -accel)
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
			
			if(jump){
				//Debug.Log("JUMPING!!");
				jump = false;
				currentJumpSpeed = 3.0f;
			}

			if(currentJumpSpeed < jumpSpeed){
				if(Input.GetKey(KeyCode.Space) && jumpHeldDownFor > 0){
					jumpHeldDownFor -= fdt;
					jumpSpeed +=fdt * 10;
				}

				currentJumpSpeed += Time.fixedDeltaTime*jumpIncrease - currentJumpSpeed;
				tr.position += tr.up * currentJumpSpeed * fdt;
				topY = tr.position.y;
			}
			if(currentJumpSpeed >= jumpSpeed && currentJumpSpeed < jumpSpeed * hangFactor){
				currentJumpSpeed += Time.fixedDeltaTime*jumpIncrease;
				//tr.position += tr.up * 9.8f * fdt;
				
				Vector3 pos = tr.position;
				pos.y = topY;
				tr.position = pos;
				
			}

			Vector3 walkDirection = (velocity.x * right + velocity.z * forward);
			if(walkDirection != Vector3.zero)
				tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(walkDirection), .5f);
			tr.position = tr.position + walkDirection;
			//tr.Translate (velocity.magnitude * camera_transform.forward);


		}
		
	}

	void OnCollisionStay(Collision collision){
		onGround = false;
		if(collision.contacts.Length >0){
			foreach(ContactPoint contact in collision.contacts){//ContactPoint contact = collision.contacts[0];
				if(Vector3.Dot(contact.normal, Vector3.up) > 0.5){
					onGround = true;
					
					jumpHeldDownFor = 0.2f;
					jumpSpeed = defaultJumpSpeed;
					
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

		if(Input.GetKeyDown(KeyCode.Space)){
			if(onGround){
				print ("jump");
				onGround = false;
				jump = true;
			}
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
		if (Input.GetAxis ("Horizontal") < 0)
			directions [2] = true;
		else if (Input.GetAxis ("Horizontal") > 0)
			directions [3] = true;

		if (Input.GetButtonDown ("Jump"))
		{
			
			velocity.y = 9.8f;
		}
		if(Input.GetButtonDown("Bark"))
		   print("bark");

		return;
	}
}
