using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {
	public Transform thisTransform;
	// Use this for initialization
	void Start () {
		thisTransform = GetComponent<Transform>();
		
		Debug.Log("Object Started");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual string getType(){
		return "Generic Object";
	}
}
