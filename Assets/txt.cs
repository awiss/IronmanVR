using UnityEngine;
using System.Collections;

public class txt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = Camera.main.transform.rotation * Vector3.forward + Camera.main.transform.position;
		Vector3 p2 = this.transform.position;
//		this.transform.position = cam;
		this.transform.rotation = Camera.main.transform.rotation;
	}
}
