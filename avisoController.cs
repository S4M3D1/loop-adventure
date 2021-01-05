using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avisoController : MonoBehaviour {

	private gameController	_GameController;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;

	}

	void OnTriggerStay2D(Collider2D c) {

		if(c.gameObject.tag == "Player")
		{
			_GameController.panels [8].SetActive (true);
		}

		if(c.gameObject.tag == "Player" && Input.GetAxisRaw("Vertical") > 0)
		{
			_GameController.panels [8].SetActive (false);
			_GameController.setGameState (gameState.ALERT);
			_GameController.panels [7].SetActive (true);
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			_GameController.panels [8].SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
