using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abysmEnemyCreator : MonoBehaviour {

	public GameObject		enemy;
	private Vector3 		position;
	public Transform 		player;
	public float 			timeToCreate;

	private int 			enemies = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		StartCoroutine ("creatEnemyController");

	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "smile")
		{
			++enemies;
		}

	}

	IEnumerator creatEnemyController()
	{
		yield return new WaitForSeconds (timeToCreate);

		position = new Vector3 (player.position.x, transform.position.y, transform.position.z);

		if(enemies > 0) {
			Instantiate(enemy, position, transform.localRotation);
			--enemies;
		}
		StopCoroutine ("creatEnemyController");
	}
}
