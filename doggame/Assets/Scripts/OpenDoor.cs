using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	Transform tr;
	bool open = false;
	int angle = 360;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(open)
		{
			angle--;
			tr.parent.RotateAround (tr.parent.position, Vector3.up, -1);
			if (angle < 270)
				enabled = false;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		open = true;
	}
}
