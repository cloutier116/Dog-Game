using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject GameObject_target;
	private Transform Transform_target;
	private Vector3 Vector3_targetPosition;
	private Quaternion Quaternion_targetRotation;

	Transform tr;
	

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		GameObject_target = GameObject.FindGameObjectWithTag("CameraTarget");
		Transform_target = GameObject_target.GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Vector3_targetPosition = Transform_target.position ;
		Quaternion_targetRotation = Transform_target.rotation;

		tr.position = Vector3.Slerp (tr.position, Vector3_targetPosition, .05f);
		//tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.LookRotation (Transform_target.position), Time.deltaTime *2);
		tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion_targetRotation, .05f);
	}
}
