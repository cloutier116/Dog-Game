using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject character;
	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		tr.position = Vector3.Lerp (tr.position, new Vector3(character.transform.position.x, character.transform.position.y + 2, character.transform.position.z - 4) , Time.deltaTime*2);
		tr.rotation = Quaternion.Slerp(tr.rotation, character.transform.rotation, Time.deltaTime);
	}
}
