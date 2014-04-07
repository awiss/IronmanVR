using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	float hitPts = 100;

	// Use this for initialization
	void Start () {
		this.renderer.material.color = new Color (0,
		                                          1, 
		                                          0, 
		                                          1.0f );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col == null)
			return;
		Vector3 impactForce = col.relativeVelocity * col.rigidbody.mass * this.rigidbody.mass;
		float force = impactForce.magnitude;
		hitPts -= force * 10.5f;
		if ( isDead () ) {
			GameObject.Find("GUI").GetComponent<guienemy>().enemyDied();
		}
		print ("force: " + force);

		this.renderer.material.color = new Color ((100-hitPts)/100,
		                                          hitPts/100, 
		                                          0, 
		                                          1.0f );
	}

	public bool isDead() {
		return hitPts <= 0;
	}
}
