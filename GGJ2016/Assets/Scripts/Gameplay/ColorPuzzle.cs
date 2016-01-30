using UnityEngine;
using System.Collections;

public class ColorPuzzle : MonoBehaviour {

    public ColorWall[] walls;
    public Color[] necesaryColors;

    public Door[] doorsToOpen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Dani: viva la pepa
	    for (int i=0; i < walls.Length; ++i)
        {
            if (!walls[i].getColor().Equals(necesaryColors[i]))
            {
                return;
            }
        }
        //completado
        for (int i = 0; i < doorsToOpen.Length; ++i)
        {
            doorsToOpen[i].open();
        }
        gameObject.SetActive(false);
	}
}
