using UnityEngine;
using System.Collections;

public class lineLaser : MonoBehaviour {

	float opacity = 1;
	LineRenderer liner;

	// Use this for initialization
	void Start () {
		opacity = 1;
		liner = this.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (opacity <= 0) {
						Destroy (this.gameObject);
				} else {
			opacity -= Time.deltaTime * 1.0f;
			/*
			Color c = new Color(liner.material.color.r,
			                    liner.material.color.g,
			                    liner.material.color.b,
			                    opacity);
			                    */
			//liner.SetColors (c,c);
		}
	}
}
