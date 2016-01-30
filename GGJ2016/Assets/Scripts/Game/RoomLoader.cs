using UnityEngine;
using System.Collections;

public class RoomLoader : MonoBehaviour {

    [Tooltip("Punto donde aparece el heroe")]
    public Transform playerSpawn;
    [Tooltip("Mapa que tiene que cargar")]
    public GameObject mapToLoad;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.changeMap(mapToLoad, playerSpawn);
    }
}
