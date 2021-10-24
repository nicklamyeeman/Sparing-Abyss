using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerInventory : MonoBehaviour
{
	public GameObject leftHand;
	public GameObject rightHand;

	public Text leftHold;
	public RawImage leftFill;

	public Text rightHold;
	public RawImage rightFill;

	private float throwForceMagnitude = 200f;
	private float fillCharge = 0f;

	void Start()
	{
		leftHold.gameObject.SetActive(false);
		rightHold.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.A) && leftHand.transform.childCount > 0)
			chargeThrow(leftFill);
		if (Input.GetKeyUp(KeyCode.A) && leftHand.transform.childCount > 0)
			dropObject(leftHand, leftFill);
	
		if (Input.GetKey(KeyCode.E) && rightHand.transform.childCount > 0)
			chargeThrow(rightFill);
		if (Input.GetKeyUp(KeyCode.E) && rightHand.transform.childCount > 0)
			dropObject(rightHand, rightFill);

		if (leftHand.transform.childCount > 0)
			leftHold.gameObject.SetActive(true);
		else
			leftHold.gameObject.SetActive(false);

		if (rightHand.transform.childCount > 0)
			rightHold.gameObject.SetActive(true);
		else
			rightHold.gameObject.SetActive(false);
	}

	void chargeThrow(RawImage fill)
	{
		throwForceMagnitude += (throwForceMagnitude < 1000f) ? 20f : 0f;
		fillCharge += (fillCharge < 300f) ? 7.5f : 0f;
		fill.GetComponent<RectTransform>().sizeDelta = new Vector2(25, fillCharge);
	}

	public bool checkHandsFull()
	{
		if (leftHand.transform.childCount + rightHand.transform.childCount >= 2)
			return true;
		return false;
	}

	public GameObject freeHand()
	{
		if (!checkHandsFull()) {
			if (leftHand.transform.childCount == 0)
				return leftHand;
			return rightHand;
		}
		return null;
	}

	void dropObject(GameObject hand, RawImage fill)
	{
		hand.transform.GetChild(0).GetComponent<BoxCollider>().enabled = !hand.transform.GetChild(0).GetComponent<BoxCollider>().enabled;
		hand.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
	
		Vector2 force = transform.GetChild(0).transform.forward * throwForceMagnitude;
	    hand.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(force);

		hand.transform.GetChild(0).transform.parent = transform.parent;

		throwForceMagnitude = 200f;
		fillCharge = 0f;
		fill.GetComponent<RectTransform>().sizeDelta = new Vector2(25, fillCharge);

	}

	public void Myprint()
	{
		Debug.Log(leftHand.transform.childCount);
		Debug.Log(rightHand.transform.childCount);
	}

}
