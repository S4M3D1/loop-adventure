using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletableDestroy : MonoBehaviour {

	public float time;

	// Use this for initialization
	void Start () {
		StartCoroutine ("destroy");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator destroy()
	{
		yield return new WaitForSeconds (time);
		Destroy (this.gameObject);
	}

}
