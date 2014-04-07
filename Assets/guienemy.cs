using UnityEngine;
using System.Collections;

public class guienemy : MonoBehaviour {

	public int enemies;

	GUIText eleft;
	
	// Use this for initialization
	void Start () {
		GUIText[] ts = this.GetComponents<GUIText> ();
		eleft = ts [0];
		eleft.text = "Enemies Left: " + enemies;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		//GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
	}
	public void enemyDied() {
		enemies--;
	}
}
