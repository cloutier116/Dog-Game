using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject GameObject_target;
	public Movement Movement_target;
	public Transform Transform_target;
	public Vector3 Vector3_targetPosition;
	public Quaternion Quaternion_targetRotation;

	public bool frozen = false;
	
	public GameObject GameObject_climbingTarget;
	public Transform Transform_climbingTarget;
	public Vector3 Vector3_climbingTargetPosition;
	public Quaternion Quaternion_climbingTargetRotation;
	

	Transform tr;
	

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		GameObject_target = GameObject.FindGameObjectWithTag("CameraTarget");
		Transform_target = GameObject_target.GetComponent<Transform>();
		Movement_target = GameObject.FindGameObjectWithTag("MasterPlayer").GetComponent<Movement>();

		
		GameObject_climbingTarget = GameObject.FindGameObjectWithTag("ClimbingCameraTarget");
		Transform_climbingTarget = GameObject_climbingTarget.GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

		if(frozen){
			Vector3_targetPosition = Transform_target.position;
			Quaternion_targetRotation = Transform_target.rotation;
			float distance = Vector3.Distance(Vector3_targetPosition,tr.position);
			distance += 0.5f;
			
			Quaternion currentRot = tr.rotation;
			float diffInRotation = Mathf.Abs(currentRot.x - Quaternion_targetRotation.x) +
				Mathf.Abs(currentRot.y - Quaternion_targetRotation.y) + 
					Mathf.Abs(currentRot.z - Quaternion_targetRotation.z);
			diffInRotation += 1.0f;
			
			Vector3 futurePosition = (Vector3_targetPosition - tr.position);
			//print (futurePosition);
			if(futurePosition.magnitude > .02)
			{
				futurePosition = futurePosition.normalized * 5;
				tr.position = Vector3.Lerp (tr.position, tr.position + futurePosition, Time.deltaTime*distance * 0.2f);
				tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_targetRotation, Time.deltaTime * diffInRotation *2.0f);
			}

		}
		else{
		if(Movement_target.climbing == false){
			Vector3_targetPosition = Transform_target.position;
			Quaternion_targetRotation = Transform_target.rotation;
	
			float distance = Vector3.Distance(Vector3_targetPosition,tr.position);
			distance += 0.5f;

			Quaternion currentRot = tr.rotation;
			float diffInRotation = Mathf.Abs(currentRot.x - Quaternion_targetRotation.x) +
				Mathf.Abs(currentRot.y - Quaternion_targetRotation.y) + 
				Mathf.Abs(currentRot.z - Quaternion_targetRotation.z);
			diffInRotation += 1.0f;
			
			Vector3 futurePosition = (Vector3_targetPosition - tr.position);
			//print (futurePosition);
			if(futurePosition.magnitude > .02)
			{
				futurePosition = futurePosition.normalized * 5;
				tr.position = Vector3.Lerp (tr.position, tr.position + futurePosition, Time.deltaTime*distance * 0.5f);
				tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_targetRotation, Time.deltaTime * diffInRotation *2.0f);
			}
		}
		else{
			
			Vector3_climbingTargetPosition = Transform_climbingTarget.position;
			Quaternion_climbingTargetRotation = Transform_climbingTarget.rotation;
			
			float distance = Vector3.Distance(Vector3_targetPosition,tr.position);
			distance += 0.5f;

			Vector3 futurePosition = Vector3.Lerp (tr.position, Vector3_climbingTargetPosition, 0.5f);

			tr.position = Vector3.Lerp(tr.position, (futurePosition - tr.position).normalized, .5f);
			tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_climbingTargetRotation, 4f);
		}
		}
	}
}
