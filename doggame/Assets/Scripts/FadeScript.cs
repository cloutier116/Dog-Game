using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {
	float time = 5.0f;
	private Color c;
	public bool fade = true;
	// Use this for initialization
	void Start () {
		c = renderer.material.color;
		renderer.material.color = new Color(c.r,c.g,c.b,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(fade){
			if(time > 0){
				time -= Time.deltaTime;
			}
			else{
				float alpha = c.a;
				
				if( alpha>0){
					c.a = alpha - 0.3f * Time.deltaTime;
					renderer.material.color = c;
				}
				//Debug.Log (renderer.material.color.a);
			}
		}
		else if(!fade){
			if(time > 0){
				time -= Time.deltaTime;
			}
			else{
				float alpha = c.a;
				
				if( alpha<1.0f){
					c.a = alpha + 0.3f * Time.deltaTime;
					renderer.material.color = c;
				}
				//Debug.Log (renderer.material.color.a);
			}

		}
	}
}
