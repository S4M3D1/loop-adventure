using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum musicStage
{
	FOREST, CAVE, GAMEOVER, THEEND
}

public enum  gameState
{
	TITLE, GAMEPLAY, END, GAMEOVER, MARKET, CONTRACT, PAUSE, ALERT, RESET
}

public enum stages
{
	FOREST, CAVE, SKY, END
}

class Data{

	public static int coins = 0, vidaMax = 3, I = 0;
	public static bool reset = true;

}

public class gameController : MonoBehaviour {

	public gameState	currentState;
	public GameObject[]	panels;

	private Camera 		cam;
	public Transform	playerTransform;

	public float		speedCam;
	public Transform	limitCamLeft, limitCamRight, limitCamUp, limitCamDown;


	[Header("Audio")]
	public AudioSource	sfxSource;
	public AudioSource	musicSource;

	public AudioClip	sfxJump;
	public AudioClip	sfxAtack;
	public AudioClip	sfxFootRight;
	public AudioClip	sfxFootLeft;
	public AudioClip	sfxCoin;
	public AudioClip	sfxEnemyDead;
	public AudioClip	sfxDamage;
	public AudioClip[]	sfxStep;

	public GameObject[]	stage;

	public stages 		stageSelected;

	public musicStage	musicSelected;

	public AudioClip	musicForest, musicCave, musicGameOver, musicEnd;

	public int			vida, coins, vidaMax, I;
	public Text 		coinsTxt, coinsXTxt, ITxt, IXTxt;
	public Image 		invencible;
	public Image 		coin;
	public Image[] 		coracoes;
	public Image[]		vidas;

	public Rigidbody2D	playerRB;
	public Animator 	playerAnimator;
	public Transform[] 	finish;
	private bool		finished = false;

	// Use this for initialization
	void Start () {

		cam = Camera.main;

		vidaMax = Data.vidaMax;
		getCoin (Data.coins);
		getInvencible (Data.I);

		vida = vidaMax;
		heartController ();

		foreach(GameObject o in panels)
		{
			o.SetActive (false);
		}
		if (Data.reset) {
			panels [0].SetActive (true);
		} else if (!Data.reset) {
			setGameState (gameState.MARKET);
		} 


		foreach(GameObject o in stage)
		{
			o.SetActive (false);
		}

		stage[0].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState == gameState.TITLE && Input.GetButtonDown("Submit")) {

			panels [0].SetActive (false);
			setGameState (gameState.GAMEPLAY);

		} else if (currentState == gameState.CONTRACT) {
			if (Input.GetButtonDown("Submit")) {
				panels [2].SetActive (false);
				continuar ();
				setGameState (gameState.TITLE);
			} else if (Input.GetButtonDown("Cancel")) {
				panels [2].SetActive (false);
				setGameState (gameState.GAMEOVER);
			}
		} else if (currentState == gameState.GAMEPLAY && Input.GetButtonDown("Pause")) {
			setGameState (gameState.PAUSE);
		} else if (currentState == gameState.PAUSE && Input.GetButtonDown("Pause")) {

			foreach (GameObject p in panels) {
				p.SetActive (false);
			}

			Time.timeScale = 1;
			setGameState (gameState.GAMEPLAY);
		} else if (currentState == gameState.ALERT && Input.GetAxisRaw("Vertical") < 0) {

			foreach (GameObject p in panels) {
				p.SetActive (false);
			}

			Time.timeScale = 1;
			setGameState (gameState.GAMEPLAY);
		} else if (currentState == gameState.END) {

			if(!finished) {
				playerRB.velocity = new Vector2 (2, playerRB.velocity.y);
				playerAnimator.SetInteger ("horizontal", 2);
			}

			if(playerTransform.position.x > finish[0].position.x && !finished){
				Time.timeScale = 0;
				panels [5].SetActive (true);
				finished = true;
				playerTransform.localScale = new Vector3(playerTransform.transform.localScale.x * -1, playerTransform.transform.localScale.y, playerTransform.transform.localScale.z);
				playerRB.velocity = new Vector2 (0, playerRB.velocity.y);
				playerAnimator.SetInteger ("horizontal", 0);
			}

			if(Input.GetButtonDown("Submit") && finished) {
				Time.timeScale = 1;
				panels [5].SetActive (false);
			}

			if(finished && playerTransform.position.x > finish [1].position.x) {
				playerRB.velocity = new Vector2 (-2, playerRB.velocity.y);
				playerAnimator.SetInteger ("horizontal", 2);
			}

			if (playerTransform.position.x < finish [1].position.x) {
				panels [6].SetActive (true);
				playerTransform.gameObject.SetActive (false);
				Time.timeScale = 0;
			}

			if (playerTransform.position.x < finish [1].position.x && Input.GetButtonDown("Submit")) {
				Time.timeScale = 1;
				Data.reset = true;
				setGameState (gameState.RESET);
			}
		}
	}

	void LateUpdate() {

		//camera sempre seguindo o personagem
		//Vector3 posCam = new Vector3 (playerTransform.position.x, playerTransform.position.y, cam.transform.position.z);
		//cam.transform.position = posCam;

		float posCamX = playerTransform.position.x;
		float posCamY = playerTransform.position.y;

		if(cam.transform.position.x < limitCamLeft.position.x && playerTransform.position.x < limitCamLeft.position.x)
		{
			posCamX = limitCamLeft.position.x;
		} 
		else if(cam.transform.position.x > limitCamRight.position.x && playerTransform.position.x > limitCamRight.position.x)
		{
			posCamX = limitCamRight.position.x;
		}

		if(cam.transform.position.y < limitCamDown.position.y && playerTransform.position.y < limitCamDown.position.y)
		{
			posCamY = limitCamDown.position.y;
		}
		else if(cam.transform.position.y > limitCamUp.position.y && playerTransform.position.y > limitCamUp.position.y)
		{
			posCamY = limitCamUp.position.y;
		}

		Vector3 posCam = new Vector3 (posCamX, posCamY, cam.transform.position.z);
		cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
	}

	public void playSFX(AudioClip sfxClip, float volume)
	{
		sfxSource.PlayOneShot (sfxClip, volume);
	}

	public void	changeMusic(musicStage newMusic)
	{
		AudioClip clip = null;

		switch (newMusic)
		{
		case musicStage.CAVE:
			clip = musicCave;
			break;
		case musicStage.FOREST:
			clip = musicForest;
			break;
		case musicStage.GAMEOVER:
			clip = musicGameOver;
			break;
		case musicStage.THEEND:
			clip = musicEnd;
			break;
		}

		StartCoroutine ("controllerMusic", clip);
	}

	public void changeStage (stages newStage)
	{

		switch(newStage)
		{
		case stages.FOREST:
			stage[0].SetActive (true);
			break;
		case stages.CAVE:
			stage[1].SetActive (true);
			break;
		case stages.SKY:
			stage[2].SetActive (true);
			break;
		case stages.END:
			stage[3].SetActive (true);
			break;
		}

		switch(stageSelected)
		{
		case stages.FOREST:
			stage[0].SetActive (false);
			break;
		case stages.CAVE:
			stage[1].SetActive (false);
			break;
		case stages.SKY:
			stage[2].SetActive (false);
			break;
		case stages.END:
			stage[3].SetActive (false);
			break;
		}

		stageSelected = newStage;

	}

	IEnumerator controllerMusic(AudioClip music)
	{
		float volMax = musicSource.volume;
		for(float vol = volMax; vol > 0; vol -= 0.01f)
		{
			musicSource.volume = vol;
			yield return new WaitForEndOfFrame ();
		}

		musicSource.clip = music;
		musicSource.Play ();

		for(float vol = 0; vol < volMax; vol += 0.01f)
		{
			musicSource.volume = vol;
			yield return new WaitForEndOfFrame ();
		}
	}

	public void getCoin(int n)
	{
		coins += n;
		coinsTxt.text = coins.ToString ();
	}

	public void getInvencible(int n)
	{
		I += n;
		ITxt.text = I.ToString ();
	}

	public void getHit()
	{
		vida -= 1;
		heartController ();
		if (vida <= 0) 
		{
			gameOver ();
		}
	}

	public void heartController()
	{
		foreach (Image h in vidas)
		{
			h.enabled = false;
		}

		for(int i = 0; i < vidaMax; i++)
		{
			vidas [i].enabled = true;
		}

		foreach (Image h in coracoes)
		{
			h.enabled = false;
		}

		for (int v = 0; v < vida; v++)
		{
			coracoes[v].enabled = true;
		}
	}

	public void gameOver()
	{
		playSFX (sfxDamage, 2f);
		playerTransform.gameObject.SetActive (false);
		setGameState (gameState.GAMEOVER);
		changeMusic (musicStage.GAMEOVER);
	}

	public void setGameState(gameState state)
	{
		if (state == gameState.TITLE) {

			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

		} if (state == gameState.RESET) {

			SceneManager.LoadScene ("menu");

		} else if (state == gameState.CONTRACT) {

			currentState = gameState.CONTRACT;
			panels [2].SetActive (true);

		} else if (state == gameState.MARKET) {

			currentState = gameState.MARKET;
			panels [3].SetActive (true);

		} else if (state == gameState.GAMEOVER) {

			currentState = gameState.GAMEOVER;
			panels [1].SetActive(true);

		} else if (state == gameState.GAMEPLAY) {

			currentState = gameState.GAMEPLAY;

		} else if (state == gameState.PAUSE) {
			
			Time.timeScale = 0;
			currentState = gameState.PAUSE;
			panels [4].SetActive(true);

		} else if (state == gameState.ALERT) {

			Time.timeScale = 0;
			currentState = gameState.ALERT;

		} else if (state == gameState.END) {

			currentState = gameState.END;

			foreach(GameObject o in stage)
			{
				o.SetActive (false);
			}
			stage[3].SetActive (true);

			changeMusic (musicStage.THEEND);

			limitCamRight.position = new Vector3 (10f, limitCamRight.position.y, limitCamRight.position.z);

			foreach (Image h in coracoes) {
				h.enabled = false;
			}
			foreach (Image h in vidas) {
				h.enabled = false;
			}
			invencible.enabled = false;
			coin.enabled = false;
			coinsTxt.enabled = false;
			coinsXTxt.enabled = false;
			ITxt.enabled = false;
			IXTxt.enabled = false;

		}

	}

	public void continuar()
	{
		Data.coins = coins;
		Data.vidaMax = vidaMax;
		Data.I = I;
		Data.reset = false;
	}

	public void zerarData()
	{
		Data.coins = 0;
		Data.vidaMax = 3;
		Data.I = 0;
		Data.reset = true;
	}

	public bool getDataReset(){
		return Data.reset;
	}
}
