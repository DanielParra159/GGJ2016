using UnityEngine;
using System.Collections;

public class KillEnemiesPuzzle : MonoBehaviour {


    public Enemy[] enemies;
    public bool sort = false;
    public bool fail = false;
    protected int currentEnemyToTest = 0;
    protected int currentEnemiesDead = 0;

    public Door[] doorsToOpen;

    protected bool firstTime = true;


	// Use this for initialization
    void OnEnable()
    {
        if (firstTime)
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].GetComponent<Life>().registerOnDead(onDead);
            }
        }
        else
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                //enemies[i].Reset();
            }
        }
        firstTime = false;
        currentEnemyToTest = 0;
        fail = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onDead()
    {
        if (fail) return;
        if (sort)
        {
            if (enemies[currentEnemyToTest].GetComponent<Life>().isAlive())
            {
                //fallo
                fail = true;
            }
        }
        if (++currentEnemiesDead == enemies.Length)
        {
            ++currentEnemyToTest;
            //completado
            for (int i = 0; i < doorsToOpen.Length; ++i)
            {
                doorsToOpen[i].open();
            }
            gameObject.SetActive(false);
        }
    }
}
