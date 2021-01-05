using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batIA : MonoBehaviour {

	private gameController	_GameController;
	private Animator		batAnimator;

	private bool			isFolow;

	public GameObject		hitBox;

	public float			speed;
	public bool				isLookLeft;

	public GameObject[] 	gifts;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;

		batAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(_GameController.currentState != gameState.GAMEPLAY)
		{
			return;
		}

		if (transform.position.x < _GameController.playerTransform.position.x && isLookLeft)
		{
			Flip ();
		}
		else if (transform.position.x > _GameController.playerTransform.position.x && !isLookLeft)
		{
			Flip ();
		}

	}

	void OnBecameVisible() {
		
		isFolow = true;
		StartCoroutine("batFly");

	}

	void OnBecameInvisible() {

		StopCoroutine("batFly");
		isFolow = false;

	}

	void OnTriggerEnter2D (Collider2D c) {

		if(c.gameObject.tag == "hitBox") 
		{
			speed = 0;
			StopCoroutine("batFly");
			Destroy (hitBox);
			_GameController.playSFX (_GameController.sfxEnemyDead, 0.3f);
			batAnimator.SetTrigger ("die");
			gift ();
		}

	}

	IEnumerator batFly ()
	{
		if(isFolow)
		{
			transform.position = Vector3.MoveTowards (transform.position, _GameController.playerTransform.position, speed * Time.deltaTime);
		}

		yield return new WaitForEndOfFrame ();

		StartCoroutine("batFly");
	}

	void Flip()
	{
		isLookLeft = !isLookLeft;
		float x = transform.localScale.x * -1; // inverte o sinal do scale X
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	void OnDead () 
	{
		Destroy (this.gameObject);
	}

	void gift ()
	{

		int rand = Random.Range (0, 100);

		if (rand < 10)
		{ }
		if(rand < 70)
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

}
