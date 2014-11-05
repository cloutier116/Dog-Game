//#define debugMode
using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {
	private Transform myTransform;
	public GameObject holding; //gameobject the player is currently holding
	public float radius = 1.0f; 
	public float timeSinceBark = 0.0f;
	public float nextBark = 30.0f;
	public AudioClip bark;
	public AudioClip bark1;
	public AudioClip bark2;

	public AudioClip squeak;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}

	void Bark(){
		timeSinceBark = 0.0f;
		nextBark = Random.Range(10.0f,30.0f);
		int choiceBark = Random.Range(1,4);
		switch(choiceBark){
		case 1:
			audio.PlayOneShot(bark, Random.Range(0.2f,0.9f));
			break;
		case 2:
			audio.PlayOneShot(bark1, Random.Range(0.2f,0.9f));
			break;
		case 3:
			audio.PlayOneShot(bark2, Random.Range(0.2f,0.9f));
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceBark += Time.fixedDeltaTime;
		if(holding)
			holding.GetComponent<Transform>().localPosition = new Vector3(0,.65f,.65f); 
		if (Input.GetButtonUp("Bark")) 
		{
			Bark();
			//bark
		}

		if (Input.GetButtonUp("Interact"))
		{
			if(holding){
				holding.GetComponent<Collider>().enabled =false;
				holding.GetComponent<Transform>().parent = null;
//				holding.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				holding = null;
			}
			else
			{
	#if debugMode
				Debug.Log ("G pressed, attempting to interact");
	#endif
				Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, 2);
				int i = 0;
				Debug.Log ("hitColliders.Length:" + hitColliders.Length);
				while (i < hitColliders.Length) {
					//hitColliders[i].SendMessage("AddDamage");
					if(hitColliders[i].tag == "Interactive"){
	#if debugMode
						Debug.Log ("Interactive object is in range");
	#endif
						audio.PlayOneShot(squeak);
						holding = hitColliders[i].gameObject;
						Object Object_target = holding.GetComponent<Object>();
						Object.type type =  Object_target.getType();
	#if debugMode1
						Debug.Log ("type:" + type);
	#endif
						if(type == Object.type.TYPE_PICKUP){
							Debug.Log ("picking up object");
							Transform Transform_Target = Object_target.GetComponent<Transform>();
							//Transform_Target.transform.position = Transform_Target.position + Transform_Target.forward*100;
							Transform_Target.parent = myTransform;
							Object_target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
							Transform_Target.localPosition = new Vector3(0,.65f,2.6f);
							Transform_Target.GetComponent<Collider>().enabled =false;

						}
					}
					i++;
				}
			}
		}

		if(timeSinceBark > nextBark){
			Bark ();
		}
	}
}
