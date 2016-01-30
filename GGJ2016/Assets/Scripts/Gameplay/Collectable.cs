using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public enum COLLECTABLES
    {
        COLLECTABLE_01, COLLECTABLE_02, COLLECTABLE_03, COLLECTABLE_04, COUNT, NONE
    }

    [Tooltip("Tipo del objeto")]
    public COLLECTABLES type;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Hero>().pickItem(type);
    }
}
