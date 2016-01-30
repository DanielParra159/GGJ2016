using UnityEngine;
using System.Collections;

public class KillHeroe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();
        if (hero == null) return;
        hero.GetComponent<Life>().OnDamage(float.MaxValue);
    }
}
