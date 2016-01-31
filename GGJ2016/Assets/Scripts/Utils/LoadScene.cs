using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnEnable()
    {
        Application.LoadLevel(4);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
