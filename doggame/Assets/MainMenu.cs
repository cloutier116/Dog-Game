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
		
		if(GUI.Button(new Rect(Screen.width/2-400,Screen.height/2-400,800,800), image)){
			Application.LoadLevel(1);
		}
	}
}
