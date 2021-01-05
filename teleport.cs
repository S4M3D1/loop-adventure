using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {

	private gameController	_GameController;

	public	Transform 		pontoSaida;
	public	Transform 		posCam;

	public Transform		limitCamLeft, limitCamRight, limitCamUp, limitCamDown;

	public musicStage 		newMusic;
	public stages 			newStage;
	public bool 			isNewStage;
	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;


	}
	
	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "Player" && isNewStage)
		{
			_GameController.changeStage (newStage);
			_GameController.changeMusic (newMusic);
			c.transform.position = pontoSaida.position;
			Camera.main.transform.position = posCam.position;

			_GameController.limitCamDown = limitCamDown;
			_GameController.limitCamUp = limitCamUp;
			_GameController.limitCamRight = limitCamRight;
			_GameController.limitCamLeft = limitCamLeft;

		}
		else if(c.gameObject.tag == "Player" && !isNewStage)
		{
			c.transform.position = pontoSaida.position;
			Camera.main.transform.position = posCam.position;

			_GameController.limitCamDown = limitCamDown;
			_GameController.limitCamUp = limitCamUp;
			_GameController.limitCamRight = limitCamRight;
			_GameController.limitCamLeft = limitCamLeft;

		}

	}


}
