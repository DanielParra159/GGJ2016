using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [Tooltip("Llave que necesita")]
    public Collectable.COLLECTABLES key = Collectable.COLLECTABLES.NONE;

    [Tooltip("Esta abierta?")]
    public bool opened;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void open()
    {
        opened = true;
    }
    public void close()
    {
        opened = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!opened && key == Collectable.COLLECTABLES.NONE || other.GetComponent<Hero>().haveItem(key))
        {
            open();
        }
    }
}
