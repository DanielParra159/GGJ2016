using UnityEngine;
using System.Collections;

public class TalkToGoddes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        print("hablar");
        
        if (other.tag == "Player")
        {
            print("hablar");
            gameObject.GetComponent<Animator>().Play("ConversacionConBruja");

        }
    }
}
