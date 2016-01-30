using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [Tooltip("Llave que necesita")]
    public Collectable.COLLECTABLES key = Collectable.COLLECTABLES.NONE;

    [Tooltip("Esta abierta?")]
    public bool opened;

    public bool chronometer;
    public float chronometerTime = 10.0f;
    protected float currentChronometerTime = 10.0f;

	// Use this for initialization
    void OnEnable()
    {
        if (chronometer)
        {
            this.open();
            currentChronometerTime = chronometerTime;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (chronometer)
        {
            currentChronometerTime -= Time.deltaTime;
            if (currentChronometerTime < 0.0f)
            {
                this.close();
            }
        }
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
