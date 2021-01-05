using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {


	public Image		play, exit;
	public Animator		playAni, exitAni;

	public AudioSource	sfxSource;
	public AudioSource	musicSource;
	public AudioClip	sfxJump;

	private Camera 		cam;
	public float		speedCam;
	public Transform	limitCamLeft, limitCamRight, menuTransform;

	private bool 		_lastInputAxisState;

	// Use this for initialization
	void Start () {

		cam = Camera.main;

		exit.enabled = false;
		playAni.SetBool ("selected", true);

	}
	
	// Update is called once per frame
	void Update () {

		if (play.enabled) {

			if(GetAxisInputLikeOnKeyDown("Vertical")){

				//Input.ResetInputAxes ();

				playSFX (sfxJump, 0.5f);
				play.enabled = false;
				playAni.SetBool ("selected", false);
				exit.enabled = true;
				exitAni.SetBool ("selected", true);	
			}

			if (Input.GetButtonDown("Submit")) {
				SceneManager.LoadScene ("save");
			}

		} else if (exit.enabled) {

			if(GetAxisInputLikeOnKeyDown("Vertical")) {

				//Input.ResetInputAxes ();

				playSFX (sfxJump, 0.5f);
				exit.enabled = false;
				exitAni.SetBool ("selected", false);
				play.enabled = true;
				playAni.SetBool ("selected", true);
			}

			if (Input.GetButtonDown("Submit")) {
				Application.Quit ();
			}
		}

	}

	void LateUpdate() {

		float posCamX = menuTransform.position.x;

		if(cam.transform.position.x < limitCamLeft.position.x && menuTransform.position.x < limitCamLeft.position.x)
		{
			posCamX = limitCamLeft.position.x;
		} 
		else if(cam.transform.position.x > limitCamRight.position.x && menuTransform.position.x > limitCamRight.position.x)
		{
			posCamX = limitCamRight.position.x;
		}

		Vector3 posCam = new Vector3 (posCamX, cam.transform.position.y, cam.transform.position.z);
		cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);

	}

	public void playSFX(AudioClip sfxClip, float volume)
	{
		sfxSource.PlayOneShot (sfxClip, volume);
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
