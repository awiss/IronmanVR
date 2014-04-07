using UnityEngine;
using System.Collections;

public class newenemy : MonoBehaviour {

	public float moveSpeed = 4f;
	GameObject man;
	GameObject psys;

	float killMe = -1000;
	bool isDying = false;

	// Use this for initialization
	void Start () {
		man = GameObject.Find ("MAN");
		killMe =-1000;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDying) {
			Vector3 move = (man.transform.position - transform.position).normalized
					* Time.deltaTime * moveSpeed;


			this.transform.rotation = Quaternion.LookRotation (move);

			this.transform.position = transform.position + move;

			if (Vector3.Distance (transform.position, man.transform.position) < 3.39627f) {
					((manmove)man.GetComponent<manmove> ()).takeDamage ();
					GameObject.Destroy (gameObject);
			}
				}

		if (killMe >= 0) {
			killMe -= Time.deltaTime;
		} else if (killMe >= -500) {
			//Destroy(this.gameObject);
		}

	}

	public void wasHit() {
		Destroy (this.gameObject);
		
//		psys.particleSystem.Play ();
//		this.transform.GetChild (0).particleSystem.Play();
//		killMe = 15;
//		isDying = true;
	}
}
