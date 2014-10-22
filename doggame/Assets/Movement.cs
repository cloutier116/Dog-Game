using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 velocity = new Vector3(0,0,0);
	public float accel = 1f;
	public bool[] directions = new bool[] {false, false, false, false};
									//     forward, back, left, right
	public float maxVel = 10f;
	public Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		getInput ();

		if(directions[0])
			velocity.z += accel;
		else if(directions[1])
			velocity.z -= accel;
		else
		{
			if(velocity.z < accel/2 || velocity.z > -accel/2)
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
			if(velocity.x < accel/2 || velocity.x > -accel/2)
				velocity.x = 0;
			else if(velocity.x > 0)
				velocity.x -= accel/2;
			else if(velocity.x < 0)
				velocity.x += accel/2;
		}

		velocity = Vector3.ClampMagnitude (velocity, maxVel);

		tr.LookAt (tr.position + velocity);
		transform.Translate (0, 0, velocity.magnitude);
	}

	void getInput()
	{
		for(int i = 0; i < directions.Length; i++)
			directions[i] = false;

		if (Input.GetAxis ("Vertical") > 0)
			directions [0] = true;
		else if(Input.GetAxis("Vertical") < 0)
			directions [1] = true;
		if (Input.GetAxis ("Horizontal") < 0)
			directions [2] = true;
		else if (Input.GetAxis ("Horizontal") > 0)
			directions [3] = true;
		return;
	}
}
