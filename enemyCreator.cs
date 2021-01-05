using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCreator : MonoBehaviour {

	public GameObject		enemy;
	private Vector3 		position;
	public Transform 		player;
	public float 			timeToCreate;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
		StartCoroutine ("creatEnemyController");

	}

	IEnumerator creatEnemyController()
	{
		yield return new WaitForSeconds (timeToCreate);

		position = new Vector3 (player.position.x, transform.position.y, transform.position.z);

		Instantiate(enemy, position, transform.localRotation);
		StopCoroutine ("creatEnemyController");
	}

}
