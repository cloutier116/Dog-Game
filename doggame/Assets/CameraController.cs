using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject GameObject_target;
	private Movement Movement_target;
	private Transform Transform_target;
	private Vector3 Vector3_targetPosition;
	private Quaternion Quaternion_targetRotation;
	
	public GameObject GameObject_climbingTarget;
	private Transform Transform_climbingTarget;
	private Vector3 Vector3_climbingTargetPosition;
	private Quaternion Quaternion_climbingTargetRotation;
	

	Transform tr;
	

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		GameObject_target = GameObject.FindGameObjectWithTag("CameraTarget");
		Transform_target = GameObject_target.GetComponent<Transform>();
		Movement_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

		
		GameObject_climbingTarget = GameObject.FindGameObjectWithTag("ClimbingCameraTarget");
		Transform_climbingTarget = GameObject_climbingTarget.GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if(Movement_target.climbing == false){
			Vector3_targetPosition = Transform_target.position;
			Quaternion_targetRotation = Transform_target.rotation;
	
			tr.position = Vector3.Lerp (tr.position, Vector3_targetPosition, Time.deltaTime);
			tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_targetRotation, Time.deltaTime*2);
		}
		else{
			
			Vector3_climbingTargetPosition = Transform_climbingTarget.position;
			Quaternion_climbingTargetRotation = Transform_climbingTarget.rotation;
			
			tr.position = Vector3.Lerp (tr.position, Vector3_climbingTargetPosition, Time.deltaTime);
			tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_climbingTargetRotation, Time.deltaTime*2);
		}
	}
}
