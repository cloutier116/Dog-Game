﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 velocity = new Vector3(0,0,0);
	public Quaternion fixedRotation = Quaternion.Euler(0f,0f,0f);
	public float accel = 1f;
	public bool[] directions = new bool[] {false, false, false, false};
									//     forward, back, left, right
	public float maxVel = 10f;
	public bool climbing;
	public float tsb = 0.0f;
	public Transform tr;
	public GameObject camera;
	public Transform camera_transform;
	public bool jump = false;

	public float currentJumpSpeed = 0f;

	public float jumpSpeed = 20f;

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
					velocity.z -= accel/2;
				else if(velocity.z < 0)
					velocity.z += accel/2;
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
					velocity.x -= accel/2;
				else if(velocity.x < 0)
					velocity.x += accel/2;
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
				Debug.Log("JUMPING!!");
				jump = false;
				currentJumpSpeed = 1.0f;
			}

			if(currentJumpSpeed < jumpSpeed){
				currentJumpSpeed += Time.fixedDeltaTime*5.0f;
				tr.position += tr.up * currentJumpSpeed * Time.fixedDeltaTime;
				
			}

			Vector3 walkDirection = (velocity.x * right + velocity.z * forward);
			tr.LookAt (tr.position + walkDirection);
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
