using UnityEngine;
using System.Collections;

public class CircularKiller : MonoBehaviour {

    public CircularPuzzle parent;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        parent.desactivateSecretObject();
    }
}
