using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") { //let him use the ladder
					//disable gravity
				}
	}

	void OnTriggerExit(Collider c){
		if (c.tag == "Player") { //let him use the ladder
					//enable gravity
				}
	}
}
