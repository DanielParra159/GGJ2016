using UnityEngine;
using System.Collections;

public class CollisionPuzzle : MonoBehaviour {


    public GameObject[] walls;
    protected int currentWall;

    public Door[] doorsToOpen;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        currentWall = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool onEnter(GameObject wall)
    {
        if (wall != walls[currentWall])
        {
            //fallo, reiniciar
            for (int i = 0; i < walls.Length; ++i)
            {
                walls[i].GetComponent<CollisionWall>().Reset();
            }
            currentWall = 0;
            return false;
        }

        
        if (++currentWall == walls.Length)
        {
            //completado
            for (int i = 0; i < doorsToOpen.Length; ++i)
            {
                doorsToOpen[i].open();
            }
            for (int i = 0; i < walls.Length; ++i)
            {
                walls[i].GetComponent<CollisionWall>().enabled = false;
            }
            gameObject.SetActive(false);
        }
        return true;
    }
}
