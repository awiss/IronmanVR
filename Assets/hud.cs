using UnityEngine;
using System.Collections;

public class hud : MonoBehaviour {

	TextMesh txt;
	float showTime = 4;
	float curShowTime;

	bool isShown;
	bool isAnimIn = false;
	bool isAnimOut = false;
	float opacity = 0;

	Vector3 oldPos = Vector3.zero;

	Vector3 OFFSET = new Vector3 (-.5f, 1, 2);

	
	public WWW www;
	public string notification = null;

	// Use this for initialization
	void Start () {
		opacity = 0;
		gameObject.renderer.enabled = false;
		
		txt = gameObject.GetComponent<TextMesh> ();

		
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


		Vector3 newPos = Camera.main.transform.position + 
			Camera.main.transform.rotation*OFFSET;
		float t = Time.deltaTime * 10;
		if (t > 1)
			t = 1; 
		transform.position = Vector3.Slerp (oldPos, newPos, t);
		//transform.position = newPos;

		transform.rotation = Camera.main.transform.rotation;



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
			gameObject.renderer.material.color = new Color(gameObject.renderer.material.color.r,
			                                               gameObject.renderer.material.color.g,
			                                               gameObject.renderer.material.color.b,
			                                               opacity );
		}

		oldPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
	}

	void newMessage(string message) {
		txt.text = message;
		isAnimIn = true;
		gameObject.renderer.enabled = true;
	}

}
