using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Texture image;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		
		if(GUI.Button(new Rect(Screen.width/2-Screen.width/8,Screen.height/2-Screen.height/8,Screen.width/4,Screen.height/4), image)){
			Application.LoadLevel(1);
		}
	}
}
