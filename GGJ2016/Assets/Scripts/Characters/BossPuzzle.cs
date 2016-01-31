using UnityEngine;
using System.Collections;

public class BossPuzzle : MovablePuzzle {

    public GameObject boss;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void complete()
    {
        boss.GetComponent<Boss>().shield.SetActive(false);
        boss.GetComponent<Life>().hasShield = false;
        gameObject.SetActive(false);
    }
}
