using UnityEngine;
using System.Collections;

public class PlaySadBark : MonoBehaviour {
	private bool sadBarked = false;
	public AudioSource audio;
	public AudioClip sadBark;
	public AudioClip doorOpening;
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!sadBarked && !audio.isPlaying){
			sadBarked = true;
			audio.PlayOneShot(sadBark);
		}
		else if(sadBarked && audio.isPlaying){
			audio.PlayOneShot(doorOpening);
		}
	}
}
