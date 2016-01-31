using UnityEngine;
using System.Collections;

public class TriggerMusic : MonoBehaviour {


    public AudioClip bossMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerExit(Collider other)
    {
        if (!other.Equals("Player")) return;
        if (bossMusic != null)
        {
            SoundManager.instance.SetMusic(bossMusic);
        }
    }
}
