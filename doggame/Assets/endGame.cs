using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {
	public AudioClip hb2;
	private bool happyBarked = false;
	public AudioSource audio;
	public bool door = true;
	public AudioClip doorOpening;
	public float tPassed;
	
	void Start () {
		audio = this.GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.tag);
		if(other.tag == "Player"){
			Debug.Log ("Player Collision with endgame");
			tPassed = 0.0f;
			GameObject.FindGameObjectWithTag("MasterPlayer").GetComponent<Movement>().enabled = false;
			GameObject.FindGameObjectWithTag("Curtain").GetComponent<FadeScript>().fade = false;
			if(!happyBarked && !audio.isPlaying){
				Debug.Log ("playing happy bark");
				happyBarked = true;
				audio.PlayOneShot(hb2);
			}
			
		}
	}

	void Update(){
		tPassed +=Time.deltaTime;
		if(door && happyBarked && !audio.isPlaying){
			Debug.Log ("playing door");
			audio.PlayOneShot(doorOpening);
			door = false;
			
		}
		if(tPassed > 8.0f && !door && !audio.isPlaying){
			Debug.Log ("Returning to menu");
			Application.LoadLevel(0);
		}
	}
}
