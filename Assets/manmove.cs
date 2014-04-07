using UnityEngine;
using System.Collections;

public class manmove : MonoBehaviour {

	Vector3 vel;
	Vector3 accel;
	float speed = 15;
	GUIText hp;
	int damagePerHit = 20;
	int curHp = 100;
	public int kills = 0;
	public const float totalShooting = 4.0f;
	public float shootingLeft;

	bool countdown;
	float restartTime;

	

	// Use this for initialization
	void Start () {
		shootingLeft = totalShooting;
		curHp = 100;
		hp = GameObject.Find ("HP").GetComponent<GUIText>();
		hp.text = "HP: " + curHp;
		kills = 0;
		countdown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.Space) ) {
			Application.LoadLevel(Application.loadedLevel);
		}

		if (shootingLeft < totalShooting) {
			if(shootingLeft + Time.deltaTime > totalShooting){
				shootingLeft = totalShooting;
			} else {
				shootingLeft += Time.deltaTime;
			}

		}


		Vector3 newvel = new Vector3();
		newvel.z = .1f;
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			newvel.z = 1.0f;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			newvel.z = -1.0f;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			newvel.x = -1.0f;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			newvel.x = 1.0f;
		}
		if (Input.GetKey (KeyCode.Delete)) {
			newvel.y = .7f;
				}
		

		newvel = newvel + vel * .3f;

		transform.Translate(Camera.main.transform.rotation*newvel *Time.deltaTime*speed);
		
		vel = new Vector3(newvel.x,newvel.y,newvel.z);

		if (countdown) {
			restartTime -= Time.deltaTime;
			if ( restartTime <= 0 ) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	public void takeDamage() {
		curHp -= damagePerHit;
		hp.text = "HP: " + curHp;
		if (curHp <= 0) {
			//gameOver ();
		}
	}
	public void getKill() {
		kills++;
	}

	public void gameOver() {
		GameObject.Find ("CameraLeft").GetComponent<GUILayer> ().enabled = true;
		GameObject.Find ("GUI_LOSE").GetComponent<GUITexture>().enabled = true;
		GameObject.Find ("GUI_LOSE_RESPAWN").GetComponent<GUIText>().enabled = true;
		GameObject.Find ("GUI_LOSE_RESPAWN").GetComponent<GUIText>().text = "You Lost! \n Respawning"; 

		restartTime = 2;
		countdown = true;

	}
}
