using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marketcontroller : MonoBehaviour {

	public Animator[] 		choose;

	private gameController	_GameController;

	private bool 			_lastInputAxisState;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;
		choose[0].SetBool ("selected", true);
	}
	
	// Update is called once per frame
	void Update () {

		if (choose [0].GetBool ("selected") && GetAxisInputLikeOnKeyDown("Vertical")) {
			if (Input.GetAxisRaw("Vertical") < 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[1].SetBool ("selected", true);
				choose[0].SetBool ("selected", false);
			} else if (Input.GetAxisRaw("Vertical") > 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[2].SetBool ("selected", true);
				choose[0].SetBool ("selected", false);
			}
			Input.ResetInputAxes ();
		} else if (choose [1].GetBool ("selected") && GetAxisInputLikeOnKeyDown("Vertical")) {
			if (Input.GetAxisRaw("Vertical") < 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[2].SetBool ("selected", true);
				choose[1].SetBool ("selected", false);
			} else if (Input.GetAxisRaw("Vertical") > 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[0].SetBool ("selected", true);
				choose[1].SetBool ("selected", false);
			}
			Input.ResetInputAxes ();
		} else if (choose [2].GetBool ("selected") && GetAxisInputLikeOnKeyDown("Vertical")) {
			if (Input.GetAxisRaw("Vertical") < 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[0].SetBool ("selected", true);
				choose[2].SetBool ("selected", false);
			} else if (Input.GetAxisRaw("Vertical") > 0) {
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				choose[1].SetBool ("selected", true);
				choose[2].SetBool ("selected", false);
			}
			Input.ResetInputAxes ();
		}

		if(Input.GetButtonDown("Submit"))
		{
			if(choose[0].GetBool("selected") && _GameController.coins > 9 && _GameController.vidaMax < 6)
			{
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				_GameController.getCoin (-10);
				_GameController.vidaMax += 1;
				_GameController.vida += 1;
				_GameController.heartController ();
			}
			else if(choose[1].GetBool("selected") && _GameController.coins > 0)
			{
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				_GameController.getCoin (-1);
				_GameController.getInvencible (1);
			}
			else if(choose[2].GetBool("selected") && _GameController.coins > 99)
			{
				_GameController.playSFX (_GameController.sfxJump, 0.2f);
				_GameController.getCoin (-100);
				_GameController.continuar ();
				_GameController.panels [3].SetActive (false);
				_GameController.setGameState (gameState.END);
			}

		}

		if (Input.GetButtonDown("Cancel")) {
			_GameController.panels [3].SetActive (false);
			_GameController.setGameState (gameState.GAMEPLAY);
		}

	}

	protected bool GetAxisInputLikeOnKeyDown(string axisName)
	{
		bool currentInputValue = Input.GetAxis(axisName) != 0;

		// prevent keep returning true when axis still pressed.
		if (currentInputValue && _lastInputAxisState)
		{
			return false;
		}

		_lastInputAxisState = currentInputValue;

		return currentInputValue;
	}

}
