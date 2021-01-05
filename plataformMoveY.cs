using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformMoveY : MonoBehaviour {
	
	public float 			speed;
	public int 				vertical;
	public bool 			actived;
	public float 			wait;

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

		if(c.gameObject.tag == "plataformColider")
		{
			vertical *= -1;
		}
		else if(c.gameObject.tag == "destructor")
		{
			Destroy(this.gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D c){

		if(!actived && _player.playerIsGrounded() && c.gameObject.tag == "Player")
		{
			actived = true;
		}

	}

	IEnumerator moveController()
	{
		yield return new WaitForSeconds (wait);
		plattaformRB.velocity = new Vector2 (plattaformRB.velocity.x, vertical * speed);
	}

}
