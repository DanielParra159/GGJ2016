using UnityEngine;
using System.Collections;

public class CircularTrigger : MonoBehaviour {

    public bool isFirstCircle;
    public CircularPuzzle parent;
    

	// Use this for initialization
	void Start () {
        parent = GameObject.FindObjectOfType<CircularPuzzle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("CircleElement")) return;
        if (isFirstCircle)
        {
            parent.firstCircleEnter(other.gameObject.GetComponent<Renderer>());
        }
        else
        {
            parent.secondCircleEnter(other.gameObject.GetComponent<Renderer>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("CircleElement")) return;
        if (isFirstCircle)
        {
            parent.firstCircleExit(other.gameObject.GetComponent<Renderer>());
        }
        else
        {
            parent.secondCircleExit(other.gameObject.GetComponent<Renderer>());
        }
    }
}
