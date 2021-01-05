using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abysmController : MonoBehaviour {

	public float 			speed;

	private playerController	_player;
	public bool 			isGrounded;

	private Rigidbody2D		plattaformRB;

	// Use this for initialization
	void Start () {

		_player = FindObjectOfType (typeof(playerController)) as playerController;

		plattaformRB = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {


		float vertical = Input.GetAxisRaw ("Vertical");

		if(isGrounded)
		{
			vertical = 0;
		}

		plattaformRB.velocity = new Vector2 (plattaformRB.velocity.x, vertical * speed);

		isGrounded = _player.playerIsGrounded ();
	}
		
}