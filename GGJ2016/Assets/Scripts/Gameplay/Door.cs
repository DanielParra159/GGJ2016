﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour {

    [Tooltip("Llave que necesita")]
    public Collectable.COLLECTABLES key = Collectable.COLLECTABLES.NONE;

    [Tooltip("Esta abierta?")]
    public bool opened;

    protected BoxCollider collider;
    public bool chronometer;
    public float chronometerTime = 10.0f;
    protected float currentChronometerTime = 10.0f;

    public AudioClip m_onOpen;
    public AudioClip m_onClose;

    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponent<Animator>().SetBool("Open",opened);

        if (opened){
            gameObject.GetComponent<Animator>().Play("AlreadyOpenHor");
            gameObject.GetComponent<Animator>().Play("AlreadyOpenVer");
                
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("AlreadyCloseHor");
            gameObject.GetComponent<Animator>().Play("AlreadyCloseVer");

        }

        
    }
	// Use this for initialization
    void OnEnable()
    {
        gameObject.GetComponent<Animator>().SetBool("Open", opened);

        if (opened)
        {
            gameObject.GetComponent<Animator>().Play("AlreadyOpenHor");
            gameObject.GetComponent<Animator>().Play("AlreadyOpenVer");

        }
        else
        {
            gameObject.GetComponent<Animator>().Play("AlreadyCloseHor");
            gameObject.GetComponent<Animator>().Play("AlreadyCloseVer");

        }
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
        collider.enabled = false;
        gameObject.GetComponent<Animator>().Play("OpenDoorVer");
        gameObject.GetComponent<Animator>().Play("OpenDoorHor");
        gameObject.GetComponent<Animator>().SetBool("Open",true);
        if (m_onOpen != null)
        {
            SoundManager.instance.PlaySingle(m_onOpen);
        }
    }
    public void close()
    {
        opened = false;
        collider.enabled = true;
        gameObject.GetComponent<Animator>().Play("CloseDoorHor");
        gameObject.GetComponent<Animator>().Play("CloseDoorVer");
        gameObject.GetComponent<Animator>().SetBool("Open", false);


        if (m_onClose != null)
        {
            SoundManager.instance.PlaySingle(m_onClose);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!opened && key == Collectable.COLLECTABLES.NONE || other.GetComponent<Hero>().haveItem(key))
        {
            open();
        }
    }
}
