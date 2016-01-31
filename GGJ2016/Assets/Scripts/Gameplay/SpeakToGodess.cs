using UnityEngine;
using System.Collections;

public class SpeakToGodess : MonoBehaviour {

	public Canvas myCanvas1;

	protected int fileNum;
//	protected string[] files = {"TextoTitania","TextoPortia","TextoCresidia"};
	protected Sprite spriteRenderer;
	protected bool canShowGUI;

	// Use this for initialization
	void Start () {
		fileNum = 0;
		canShowGUI = false;
		myCanvas1.gameObject.SetActive (false);
		myCanvas1.transform.GetChild (0).gameObject.SetActive(true);
		myCanvas1.transform.GetChild (1).gameObject.SetActive(true);
		myCanvas1.transform.GetChild (2).gameObject.SetActive(true);
	}

	void Update ()
	{
		if (canShowGUI && Input.GetKeyDown (KeyCode.Space)) // If the space bar is pushed down
		{
			fileNum++;
			ChangeTheDamnSprite (); // call method to change sprite
		}
	}

	void OnTriggerEnter(Collider other)
	{
		canShowGUI = true;
		myCanvas1.gameObject.SetActive (true);
	}

	void ChangeTheDamnSprite () {
		if (fileNum < 3) {
			switch (fileNum) {
			case 1:
				myCanvas1.transform.GetChild (0).gameObject.SetActive(false);
				myCanvas1.transform.GetChild (1).gameObject.SetActive(true);
				break;
			case 2:
				myCanvas1.transform.GetChild (1).gameObject.SetActive(false);
				myCanvas1.transform.GetChild (2).gameObject.SetActive(true);
				break;
			}
		} else {
			canShowGUI = false;
			myCanvas1.gameObject.SetActive (false);
		}
	}
}
