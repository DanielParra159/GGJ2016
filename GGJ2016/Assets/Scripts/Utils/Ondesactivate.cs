using UnityEngine;
using System.Collections;

public class Ondesactivate : MonoBehaviour {

    public float time = 10.0f;
    protected float currentTime;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        currentTime = time;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        if (currentTime<0.0f)
        {
            gameObject.SetActive(false);
        }
	}
}
