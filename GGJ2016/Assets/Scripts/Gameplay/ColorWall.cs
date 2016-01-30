﻿using UnityEngine;
using System.Collections;

public class ColorWall : MonoBehaviour {

    [System.Serializable]
    public class ColorRange
    {
        public float distance;
        public Color color = Color.white;
    }
    public ColorRange [] ranges;

    protected Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void collision(float distance)
    {
        for (int i = 0; i <ranges.Length; ++i)
        {
            if (distance < ranges[i].distance )
            {
                renderer.material.color = ranges[i].color;
                break;
            }
        }
    }
}