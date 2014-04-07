using UnityEngine;
using System.Collections;

public class net : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Network.Connect("127.0.0.1", 25000);
		Network.TestConnection ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
