using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularPuzzle : MonoBehaviour {

    public GameObject secretObject;

    protected List<Renderer> secondCircleRenderers;

    public Color activeColor = Color.green;
    public Color normalColor = Color.white;

	// Use this for initialization
	void Start () {
        secondCircleRenderers = new List<Renderer>();
	}
    void OnEnable()
    {
        //secretObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void secondCircleEnter(Renderer render)
    {
        secondCircleRenderers.Add(render);
        render.material.color = activeColor;
    }
    public void secondCircleExit(Renderer render)
    {
        secondCircleRenderers.Remove(render);
        render.material.color = normalColor;
    }
    public void firstCircleEnter(Renderer render)
    {
        secondCircleRenderers.Remove(render);
        render.material.color = normalColor;
    }
    public void firstCircleExit(Renderer render)
    {
        secondCircleRenderers.Add(render);
        render.material.color = activeColor;
    }
    public void desactivateSecretObject()
    {
        secretObject.SetActive(false);
    }
}
