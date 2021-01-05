using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIA : MonoBehaviour {

	private gameController	_GameController;
	private Rigidbody2D		slimeRB;
	private Animator		slimeAnimator;

	public float 			speed;
	public float 			timeToWalk;

	public GameObject		hitBox;

	public bool 			isLookLeft;
	private int 			horizontal;

	public GameObject[] 	gifts;
	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;

		slimeRB = GetComponent<Rigidbody2D>();
		slimeAnimator = GetComponent<Animator>();

		StartCoroutine("slimeWalk");
	}
	
	// Update is called once per frame
	void Update () {

		if(_GameController.currentState != gameState.GAMEPLAY)
		{
			return;
		}

		if(horizontal > 0 && isLookLeft)
		{
			Flip ();
		} else if(horizontal < 0 && !isLookLeft)
		{
			Flip ();
		}

		slimeRB.velocity = new Vector2 (horizontal * speed, slimeRB.velocity.y);

		if(horizontal != 0)
		{
			slimeAnimator.SetBool ("isWalk", true);
		}
		else
		{
			slimeAnimator.SetBool ("isWalk", false);
		}
	}

	void OnTriggerEnter2D (Collider2D c) {

		if (c.gameObject.tag == "hitBox") {
			horizontal = 0;
			StopCoroutine ("slimeWalk");
			Destroy (hitBox);
			_GameController.playSFX (_GameController.sfxEnemyDead, 0.3f);
			slimeAnimator.SetTrigger ("dead");
			gift ();
		} else if (c.gameObject.tag == "destructor") {
			Destroy (this.gameObject);
		}
	}

	IEnumerator slimeWalk ()
	{
		int rand = Random.Range (0, 100);

		if(rand < 33)
		{
			horizontal = -1;	
		}
		else if(rand < 66)
		{
			horizontal = 1;
		}
		else
		{
			horizontal = 0;
		}

		yield return new WaitForSeconds (timeToWalk);
		StartCoroutine("slimeWalk");
	}

	void gift ()
	{

		int rand = Random.Range (0, 100);

		if (rand < 10)
		{ }
		else if(rand < 80)
		{
			Instantiate (gifts[0], transform.position, gifts[0].transform.localRotation);
		}
		else if(rand < 90)
		{
			Instantiate (gifts[1], transform.position, gifts[1].transform.localRotation);
		}
		else
		{
			Instantiate (gifts[2], transform.position, gifts[2].transform.localRotation);
		}
			
	}

	void OnDead () 
	{
		Destroy (this.gameObject);
	}

	void Flip()
	{
		isLookLeft = !isLookLeft;
		float x = transform.localScale.x * -1; // inverte o sinal do scale X
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}
}
