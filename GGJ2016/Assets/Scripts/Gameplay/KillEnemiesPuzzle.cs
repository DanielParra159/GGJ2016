using UnityEngine;
using System.Collections;

public class KillEnemiesPuzzle : MonoBehaviour {


    public Enemy[] enemies;
    protected int currentEnemiesDead = 0;

    public Door[] doorsToOpen;


	// Use this for initialization
	void Start () {
	    for (int i = 0; i < enemies.Length; ++i)
        {
            enemies[i].GetComponent<Life>().registerOnDead(onDead);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onDead()
    {
        if (++currentEnemiesDead == enemies.Length)
        {
            //completado
            for (int i = 0; i < doorsToOpen.Length; ++i)
            {
                doorsToOpen[i].open();
            }
            gameObject.SetActive(false);
        }
    }
}
