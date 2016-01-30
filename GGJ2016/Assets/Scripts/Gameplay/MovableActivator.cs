using UnityEngine;
using System.Collections;

public class MovableActivator : MonoBehaviour {

    public MovablePuzzle parent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Movable movable = other.GetComponent<Movable>();
        if (movable!=null)
        {
            movable.desactiveMovable();
            parent.activeTrigger();
        }
    }
}
