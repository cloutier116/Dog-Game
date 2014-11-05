using UnityEngine;
using System.Collections;

public class StartingSounds : MonoBehaviour {
	public AudioClip whimper;
	public AudioClip doorClose;
	public bool playedDoor = false;
	public float timePassed = 0.0f;
	// Use this for initialization
	void Start () {
		audio.PlayOneShot(doorClose);
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.fixedDeltaTime;
		if(timePassed > 5.0f){
			audio.PlayOneShot(whimper);
			Destroy(this);
		}
	}
}
