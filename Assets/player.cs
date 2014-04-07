using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public GameObject ballobj;

	public GameObject previewBall;
	public GameObject previewBallAsset;

	guiball ballgui;
	// Use this for initialization
	void Start () {
		previewBall = (GameObject)Instantiate (previewBallAsset);
		previewBall.renderer.material.color = new Color (previewBall.renderer.material.color.r,
		                                                 previewBall.renderer.material.color.g, 
		                                                 previewBall.renderer.material.color.b, 
		                                                 1.0f );
		previewBall.collider.enabled = false; // disable physics obviously

		
		ballgui = (guiball)GameObject.Find ("GUI").GetComponent<guiball> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0) ) {

			Vector3 p = (Camera.main.transform.rotation * new Vector3 (0, 0, 1)) + Camera.main.transform.position;
			Quaternion rot = Camera.main.transform.rotation;
			Vector3 dir = rot * new Vector3(0,0.1f,1);
			dir *= 12;
			GameObject ball = (GameObject)Instantiate (ballobj,p, Quaternion.identity);

			ball.rigidbody.velocity = dir;

			// update ball count
			ballgui.ballShot();

		}
		// now set preview balls position in front of cam
		Vector3 ppos = (Camera.main.transform.rotation * new Vector3 (0, 0, 1)) + Camera.main.transform.position;
		previewBall.transform.position = ppos;
	}
}
