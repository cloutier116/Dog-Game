using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {
	public Transform thisTransform;
	public enum type{
		TYPE_GENERIC,
		TYPE_PICKUP,
		TYPE_COUNT
	};

	// Use this for initialization
	void Start () {
		thisTransform = GetComponent<Transform>();
		Debug.Log("Object Started");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual Object.type getType(){
		return type.TYPE_GENERIC;
	}
}
