using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {
	public float enemySpawnCooldown = 2;
	float enemySpawnCount;

	public GameObject enemyPrefab;
	GameObject man;
	public float enemyDistance = 390;

	// Use this for initialization
	void Start () {
		man = GameObject.Find ("MAN");
	}
	
	// Update is called once per frame
	void Update () {
		enemySpawnCount -= Time.deltaTime;
		if (enemySpawnCount <= 0) {
			enemySpawnCount = enemySpawnCooldown;

			Vector2 pos2d = (Random.insideUnitCircle.normalized * enemyDistance);
			Vector3 pos = new Vector3(pos2d.x + man.transform.position.x, man.transform.position.y, pos2d.y + man.transform.position.z);

			Instantiate(enemyPrefab, pos, Quaternion.identity);


		}
	}
}
