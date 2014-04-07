using UnityEngine;
using System.Collections;

public class guiball : MonoBehaviour {

	public int numBalls;

	GUIText bleft;

	// Use this for initialization
	void Start () {
		GUIText[] ts = this.GetComponents<GUIText> ();
		bleft = ts [0];
		bleft.text = "Balls Left: " + numBalls;

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
	}

	public void ballShot() {
		numBalls--;
		bleft.text = "Balls Left: " + numBalls;
	}
}
