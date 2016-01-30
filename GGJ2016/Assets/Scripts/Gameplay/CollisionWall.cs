using UnityEngine;
using System.Collections;

public class CollisionWall : MonoBehaviour {

    public CollisionPuzzle parent;

    public Color correctColor = Color.green;
    public Color normalColor = Color.white;

	// Use this for initialization
	void Start () {
	
	}
	
    public void Reset()
    {
        gameObject.GetComponent<Renderer>().material.color = normalColor;
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!this.enabled) return;
        if (parent.onEnter(this.gameObject))
        {
            gameObject.GetComponent<Renderer>().material.color = correctColor;
        }
    }
}
