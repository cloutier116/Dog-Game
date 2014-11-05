using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {
	public AudioClip hb2;
	private bool happyBarked = false;
	public AudioSource audio;
	public bool door = true;
	public AudioClip doorOpening;
	
	void Start () {
		audio = this.GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.tag);
		if(other.tag == "Player"){
			GameObject.FindGameObjectWithTag("MasterPlayer").GetComponent<Movement>().enabled = false;
			GameObject.FindGameObjectWithTag("Curtain").GetComponent<FadeScript>().fade = false;
			if(!happyBarked && !audio.isPlaying){
				happyBarked = true;
				audio.PlayOneShot(hb2);
			}
			
		}
	}

	void Update(){
		if(door && happyBarked && !audio.isPlaying){
			audio.PlayOneShot(doorOpening);
			door = false;
			
		}
		if(!door && !audio.isPlaying){
			Application.LoadLevel(0);
		}
	}
}
