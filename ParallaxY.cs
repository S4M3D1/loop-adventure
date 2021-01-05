using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxY : MonoBehaviour {
	
	public Transform	background;
	public float 		speed;


	private Transform 	cam;
	private Vector3 	previewCamPosition;

	// Use this for initialization
	void Start () {

		cam = Camera.main.transform;
		previewCamPosition = cam.position;

	}

	// Update is called once per frame
	void Update () {

		float parallaxY = previewCamPosition.y - cam.position.y;
		float bgTargetY = background.position.y + parallaxY;

		Vector3 bgPosition = new Vector3 (background.position.x, bgTargetY, background.position.z);
		background.position = Vector3.Lerp (background.position, bgPosition, speed * Time.deltaTime);

		previewCamPosition = cam.position;
	}

}
