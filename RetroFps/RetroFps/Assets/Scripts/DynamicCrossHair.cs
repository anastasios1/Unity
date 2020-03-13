using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrossHair : MonoBehaviour {

	public GameObject crosshair;
	GameObject topPart;
	GameObject bottomPart;
	GameObject leftPart;
	GameObject rightPart;

	// initial crosshair parts positions
	float initialPosition;

	//degree of crosshair spread
	static public float spread=0;

	public const int PISTOL_SHOOTING_SPREAD = 20;
	public const int JUMP_SPREAD = 50;
	public const int WALK_SPREAD = 10;
	public const int RUN_SPREAD = 25;

	void Start()
	{
		topPart = crosshair.transform.Find ("TopPart").gameObject;
		bottomPart = crosshair.transform.Find ("BottomPart").gameObject;
		rightPart = crosshair.transform.Find ("RightPart").gameObject;
		leftPart = crosshair.transform.Find ("LeftPart").gameObject;

		initialPosition = topPart.GetComponent<RectTransform> ().localPosition.y;
	}

	void Update()
	{
		//setting new positions to every crosshair part seperately

		if (spread != 0) {

			topPart.GetComponent<RectTransform> ().localPosition = new Vector3(0, initialPosition+spread, 0);
			bottomPart.GetComponent<RectTransform> ().localPosition = new Vector3(0, -(initialPosition + spread), 0);
			rightPart.GetComponent<RectTransform> ().localPosition = new Vector3(initialPosition + spread, 0, 0);
			leftPart.GetComponent<RectTransform> ().localPosition = new Vector3(-(initialPosition + spread), 0, 0); 

			spread -= 1;
		}

	}
}
