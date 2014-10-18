//#define debugMode
using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {
	private Transform myTransform;
	public GameObject holding; //gameobject the player is currently holding
	public float radius = 5.0f;
	
	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.G)) {
			if(holding != null){
				holding.GetComponent<Transform>().parent = null;
				holding = null;
			}
			else{
	#if debugMode
				Debug.Log ("G pressed, attempting to interact");
	#endif
				Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius);
				int i = 0;
				while (i < hitColliders.Length) {
					//hitColliders[i].SendMessage("AddDamage");
					if(hitColliders[i].tag == "Interactive"){
	#if debugMode
						Debug.Log ("Interactive object is in range");
	#endif
						holding = hitColliders[i].gameObject;
						Object Object_target = holding.GetComponent<Object>();
						Object.type type =  Object_target.getType();
	#if debugMode
						Debug.Log ("type:" + type);
	#endif
						if(type == Object.type.TYPE_PICKUP){
							Debug.Log ("picking up object");
							Transform Transform_Target = Object_target.GetComponent<Transform>();
							Transform_Target.parent = myTransform;

							Transform_Target.transform.position = new Vector3(Transform_Target.position.x, Transform_Target.position.y + 0.1f);
						}
					}
					i++;
				}
			}
		}
	}
}
