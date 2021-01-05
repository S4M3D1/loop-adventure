using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformMoveX : MonoBehaviour {

	public float 				speed;
	public int 					horizontal;
	public bool 				actived;
	public float 				wait;

	private Rigidbody2D		plattaformRB;

	private playerController	_player;

	// Use this for initialization
	void Start () {

		plattaformRB = GetComponent<Rigidbody2D> ();

		_player = FindObjectOfType (typeof(playerController)) as playerController;
	}

	// Update is called once per frame
	void Update () {

		if(actived)
		{
			StartCoroutine ("moveController");
		}
	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "Player" && !actived )
		{
			actived = true;
		}
		else if(c.gameObject.tag == "plataformColider")
		{
			horizontal *= -1;
		}
		else if(c.gameObject.tag == "destructor")
		{
			Destroy(this.gameObject);
		}

	}

	void OnTriggerStay2D(Collider2D c){
		if(c.gameObject.tag == "Player" && _player.playerIsGrounded())
		{
			_player.playerRB.sharedMaterial = plattaformRB.sharedMaterial;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.tag == "Player")
		{
			_player.playerRB.sharedMaterial = _player.material;
		}
	}

	IEnumerator moveController()
	{
		yield return new WaitForSeconds (wait);
		plattaformRB.velocity = new Vector2 (horizontal * speed, plattaformRB.velocity.y);
	}

}
