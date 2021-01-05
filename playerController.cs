using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private gameController		_GameController;

	public Rigidbody2D			playerRB;
	private Animator			playerAnimator;
	private SpriteRenderer		playerSR;

	public float 				speed;
	public float 				jumpForce;

	public bool					isLookLeft;

	public Transform			groundCheck;
	private bool				isGrounded;
	public LayerMask 			whatIsGround;

	private bool				isAtack;

	public Transform			hand;
	public GameObject			hitBoxPrefab;

	public Color 				hitColor;
	public Color 				invencibleColor;

	public PhysicsMaterial2D	material;

	private bool				invencible;

	// Use this for initialization
	void Start () {

		playerRB = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();
		playerSR = GetComponent<SpriteRenderer> ();

		_GameController = FindObjectOfType (typeof(gameController)) as gameController;
		_GameController.playerTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		playerAnimator.SetBool ("isGrounded", isGrounded);

		if(_GameController.currentState != gameState.GAMEPLAY)
		{
			playerRB.velocity = new Vector2 (0, playerRB.velocity.y);
			playerAnimator.SetInteger ("horizontal", 0);
			return;
		}

		float horizontal = Input.GetAxisRaw ("Horizontal");

		if(isAtack && isGrounded)
		{
			horizontal = 0;
		}

		if(horizontal > 0 && isLookLeft)
		{
			Flip ();
		}
		else if(horizontal < 0 && !isLookLeft)
		{
			Flip ();
		}

		float speedY = playerRB.velocity.y;

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			_GameController.playSFX (_GameController.sfxJump, 0.5f);
			playerRB.AddForce (new Vector2(0, jumpForce));
		}

		if(Input.GetButtonDown("Fire1") && !isAtack)
		{
			isAtack = true;
			_GameController.playSFX (_GameController.sfxAtack, 0.5f);
			playerAnimator.SetTrigger ("atack");
		}

		if(Input.GetButtonDown("Fire2") && _GameController.I > 0 && !invencible)
		{
			_GameController.getInvencible(-1);
			StartCoroutine("damageController");

		}

		playerRB.velocity = new Vector2 (horizontal * speed, speedY);

		playerAnimator.SetInteger ("horizontal", (int) horizontal);
		playerAnimator.SetFloat ("speedY", speedY);
		playerAnimator.SetBool ("isAtack", isAtack);

	}

	void FixedUpdate() {

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.01f, whatIsGround);

	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "coin")
		{
			_GameController.playSFX (_GameController.sfxCoin, 0.5f);
			_GameController.getCoin (1);
			Destroy (c.gameObject);
		}
		else if(c.gameObject.tag == "I")
		{
			_GameController.playSFX (_GameController.sfxCoin, 0.5f);
			_GameController.getInvencible (1);
			Destroy (c.gameObject);
		}
		else if(c.gameObject.tag == "life" && _GameController.vida < _GameController.vidaMax)
		{
			_GameController.playSFX (_GameController.sfxCoin, 0.5f);
			_GameController.vida += 1;
			_GameController.heartController ();
			Destroy (c.gameObject);
		}
		else if(c.gameObject.tag == "damage" && !invencible)
		{
			_GameController.getHit ();
			if(_GameController.vida > 0)
			{
				StartCoroutine("damageController");
			}
		}
		else if(c.gameObject.tag == "gameOver")
		{
			_GameController.gameOver ();
		}
		else if(c.gameObject.tag == "Respawn")
		{
			_GameController.setGameState (gameState.PAUSE);
		}

	}

	//metodos criados, nao nativos da unity
	void Flip()
	{
		isLookLeft = !isLookLeft;
		float x = transform.localScale.x * -1; // inverte o sinal do scale X
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	void OnEndedAtack()
	{
		isAtack = false;
	}

	void hitBoxAtack()
	{
		GameObject hitBoxTemp = Instantiate (hitBoxPrefab, hand.position, transform.localRotation);
		Destroy (hitBoxTemp, 0.2f);
	}

	//som dos passos aleatorio
	//void footStep()
	//{
	//	_GameController.playSFX (_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 0.5f);
	//}

	void footRight()
	{
		_GameController.playSFX (_GameController.sfxFootRight, 1f);
	}

	void footLeft()
	{
		_GameController.playSFX (_GameController.sfxFootLeft, 1f);
	}

	IEnumerator damageController()
	{
		invencible = true;

		Color standardColor = playerSR.color;

		_GameController.playSFX (_GameController.sfxDamage, 0.5f);

		this.gameObject.layer = LayerMask.NameToLayer ("Invencible");

		playerSR.color = hitColor;
		yield return new WaitForSeconds (0.3f);

		playerSR.color = invencibleColor;

		for(int i = 0; i < 5; i++)
		{
			playerSR.enabled = false;
			yield return new WaitForSeconds (0.2f);
			playerSR.enabled = true;
			yield return new WaitForSeconds (0.2f);
		}

		this.gameObject.layer = LayerMask.NameToLayer ("Player");
		playerSR.color = standardColor;

		invencible = false;
	}


	public bool playerIsGrounded()
	{
		return isGrounded;
	}

}