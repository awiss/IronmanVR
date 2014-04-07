using UnityEngine;
using System.Collections;

public class newshoot : MonoBehaviour {
	Vector3 vel;
	Vector3 lastPos;
	Vector3 accel;
	public GameObject linePrefab;
	public GameObject lineObj;
	ParticleSystem psys;
	ParticleSystem psys2;
	float prate2;
	float prate;
	float maxprate=10;
	bool lineMade = false;
	GameObject manmove;
	const float shootCooldown = .05f;
	float shootTimer;

	int ENEMY_MASK = 1<<8;
	

	// Use this for initialization
	void Start () {
		manmove = GameObject.Find("MAN");
		shootTimer = shootCooldown;
//		handPartsPrefab = Resources.Load ("HandParts");
		//handParts = Instantiate (handPartsPrefab);
		psys = transform.FindChild("HandParts").GetComponent<ParticleSystem>();
		psys2 = transform.FindChild("HandParts2").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		vel = new Vector3(transform.position.x-lastPos.x,transform.position.y-lastPos.y,transform.position.z-lastPos.z);
		lastPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		accel = accel * .9f + vel;

		/*
		if (true) {
			print (transform.parent.transform.parent.name + " " + Vector3.Distance(transform.position, transform.parent.transform.parent.position));
		}*/



		Vector3 shoulder = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.position;
		float dist = Vector3.Distance (shoulder, transform.position);
		if (dist > 1.01 ) { // was 8.4
			shootNew ();
			if ( lineObj != null ) {
				lineObj.renderer.enabled = true;
			}
			GetComponentInChildren<LensFlare>().enabled = true;
			prate += 3 * Time.deltaTime;
			if ( prate > 20 ) prate = 20;
			psys.emissionRate = prate;
			
			prate2 += 32 * Time.deltaTime;
			if ( prate2 > 70 ) prate2 = 70;
			psys2.emissionRate = prate2;
			psys2.particleEmitter.transform.rotation = Quaternion.FromToRotation(shoulder, transform.position);
		} else {
			if ( lineObj != null ) {
				transform.GetChild(0).gameObject.renderer.enabled = false;
			}
				prate -= 5 * Time.deltaTime;
				if ( prate <= 0 ) prate = 0;

				prate2 -= 70 * Time.deltaTime;
				if ( prate2 <= 0 ) prate2 = 0;

				lineObj.renderer.enabled = false;
				psys.emissionRate = prate;
				psys.emissionRate = prate2;
		}

		//if (Vector3.Dot (accel.normalized, (transform.position - GameObject.Find ("MAN").transform.position).normalized) > -.1f) {
		//if ( dist>.85f ) {
		//print ("DOT > 0");
			//print (accel);
		/*
		print (dist + " " + accel.magnitude + " " + vel.magnitude);
			// if accelerating fast, and at end of motion, and shoot time is good
			if (accel.magnitude > .5 && shootTimer <= 0) {
				shootTimer = shootCooldown;
				accel.Set (0, 0, 0);
				print ("************** SHOOT");
				shoot (); */
			//}
		//}
		
		shootTimer -= Time.deltaTime;
	}
	
	void shootNew() {
		float shootingLeft = manmove.GetComponent<manmove> ().shootingLeft;
		if( shootingLeft - Time.deltaTime*2 < 0){
			manmove.GetComponent<manmove> ().shootingLeft = 0;
		} else{
			manmove.GetComponent<manmove> ().shootingLeft -= Time.deltaTime * 2;
		}

		Vector3 dir = transform.position - transform.parent.transform.parent.position;
		dir = Vector3.Normalize (dir);

		/*
		GameObject l;
		l = GameObject.Find ("lineLaser");
		if ( l == null)
			l = (GameObject)Instantiate (linePrefab);
			*/
		if (!lineMade) {
			lineObj = (GameObject)Instantiate (linePrefab);
			lineMade = true;
		}

		LineRenderer line = lineObj.GetComponent<LineRenderer> ();
		line.SetVertexCount (2);
		line.SetPosition (0, transform.position);
		line.SetPosition (1, transform.position + dir * 30);
		line.SetWidth (.12f, .12f);
		
		Ray ray = new Ray (transform.position, dir);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 5555)) {
			if ( hit.collider.gameObject.name.Contains("Shark") ) {
				manmove.GetComponent<manmove>().getKill ();
				hit.collider.gameObject.GetComponent<newenemy>().wasHit();
			} else {
				Destroy (hit.collider.gameObject);
			}
		}
	}
}
