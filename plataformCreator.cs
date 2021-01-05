using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformCreator : MonoBehaviour {

	public GameObject		plataform;
	private Vector3 		position;
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

		position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		Instantiate(plataform, position, transform.localRotation);
		StopCoroutine ("creatEnemyController");
	}
}
