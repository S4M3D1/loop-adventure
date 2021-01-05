using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverController : MonoBehaviour {

	public Animator[] 		choose;

	private gameController	_GameController;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxisRaw("Horizontal") < 0 && !choose[0].GetBool("selected")){
			choose[1].SetBool ("selected", false);
			choose[0].SetBool ("selected", true);
		}
		else if(Input.GetAxisRaw("Horizontal") > 0 && !choose[1].GetBool("selected")){
			choose[0].SetBool ("selected", false);
			choose[1].SetBool ("selected", true);
		}

		if(Input.GetButtonDown("Submit"))
		{
			if(choose[0].GetBool("selected"))
			{
				choose[0].SetBool ("selected", false);
				_GameController.panels [1].SetActive (false);
				_GameController.setGameState (gameState.CONTRACT);
			}
			else if(choose[1].GetBool("selected") && _GameController.coins > 0)
			{
				int x = (_GameController.coins * 10) / 100;
				if (_GameController.coins < 10)
					x = 1;
				_GameController.getCoin (-x);
				_GameController.continuar ();
				_GameController.panels [1].SetActive (false);
				_GameController.setGameState (gameState.TITLE);
			}
				
		}

		if (Input.GetButtonDown("Cancel")) {
			_GameController.zerarData ();
			_GameController.panels [1].SetActive (false);
			_GameController.setGameState (gameState.RESET);
		}

	}
}
