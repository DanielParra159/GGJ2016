using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {


    public GameObject[] movableObjects;
    private Transform[] savedPositions;


    void Awake()
    {
        print("awake");

        savedPositions = new Transform[movableObjects.Length];

        for (int i = 0; i < movableObjects.Length; ++i)
        {
            Transform myTransform = movableObjects[i].transform;
            savedPositions[i] =  myTransform ;
                
                
            print(movableObjects[i].name);
        }
        
    }

   

	// Use this for initialization
	void OnEnable (){

        print("enabled");

        for (int i = 0; i < savedPositions.Length; ++i)
        {
            print("save position x");
            print(savedPositions[i].position.x);
            print("movable position x");

            print(savedPositions[i].position.x);
            movableObjects[i].transform.position.Set(savedPositions[i].position.x, savedPositions[i].position.y, savedPositions[i].position.z);
        }
        
	}


}
