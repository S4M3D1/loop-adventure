using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abysmlataformCreater : MonoBehaviour {

	public GameObject[]		plataforms;
	public Transform[] 		positions;
	public float 			timeToCreate;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		StartCoroutine ("creatPlataformController");

	}

	IEnumerator creatPlataformController()
	{
		int rand = Random.Range (0, plataforms.Length);

		yield return new WaitForSeconds (timeToCreate);

		Instantiate(plataforms[rand], positions[rand].position, positions[rand].localRotation);

		StopCoroutine ("creatPlataformController");
	}
}
