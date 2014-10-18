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
					GameObject target = hitColliders[i].gameObject.GetComponent<Object>();

				}
				i++;
			}
		}
	}
}
