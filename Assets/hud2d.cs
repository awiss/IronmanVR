using UnityEngine;
using System.Collections;

public class hud2d : MonoBehaviour {

	GUIText txt;
	float showTime = 8;
	float curShowTime;
	
	bool isShown;
	bool isAnimIn = false;
	bool isAnimOut = false;
	float opacity = 0;

	
	Vector3 OFFSET = new Vector3 (-.5f, 1, 2);

	public WWW www;
	public string notification = null;
	
	// Use this for initialization
	void Start () {
		opacity = 0;
		txt = gameObject.GetComponent<GUIText> ();
		txt.enabled = false;
		
		www = new WWW ("http://ironmanvr.herokuapp.com");
	}
	
	// Update is called once per frame
	void Update () {
		if (www.isDone) {
			if (notification == null) {
				notification = www.text;
			} else if (notification != www.text) {
				newMessage(www.text);
				notification = www.text;
			}
			www = new WWW ("http://ironmanvr.herokuapp.com");
		}
		
		if ( isAnimIn ) {
			opacity += Time.deltaTime;
			
			if ( opacity >= 1.0f ) {
				isShown = true;
				isAnimIn = false;
				opacity = 1;
				curShowTime = showTime;
			}
		}
		if (isShown) {
			curShowTime-= Time.deltaTime;
			if ( curShowTime <= 0 ) {
				isAnimOut = true;
				isShown = false;
			}
		}
		if ( isAnimOut ) {
			opacity -= Time.deltaTime;
			if ( opacity <= 0f ) {
				isAnimOut = false;
				opacity = 0;
				gameObject.renderer.enabled = false;
				
			}
		}
		
		if (isAnimOut || isAnimIn) {
			txt.color = new Color(txt.color.r,
                              		txt.color.g,
			                           txt.color.b,
			                                               opacity );
		}
	}
	
	void newMessage(string message) {
		txt.text = message;
		isAnimIn = true;
		txt.enabled = true;
	}
}
