﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
        //if (instance == null)
        {
            instance = this;
            //    DontDestroyOnLoad(instance);
        }
        /* else if (instance != this)
         {
             Destroy(this.gameObject);
         }*/
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
