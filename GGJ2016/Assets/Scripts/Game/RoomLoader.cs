using UnityEngine;
using System.Collections;

public class RoomLoader : MonoBehaviour {

    [Tooltip("Punto donde aparece el heroe")]
    public Transform playerSpawn;
    [Tooltip("Mapa que tiene que cargar")]
    public GameObject mapToLoad;

    public bool changeCheckPoint = false;
    public bool resetCheckPoint = true;

    public AudioClip m_onLoad;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.changeMap(mapToLoad, playerSpawn);
        if (changeCheckPoint)
        {
            GameManager.instance.spawnPosition = playerSpawn;
        }
        else if (resetCheckPoint)
        {
            GameManager.instance.spawnPosition = GameManager.instance.origPosition;
        }
        if (m_onLoad != null)
        {
            SoundManager.instance.PlaySingle(m_onLoad);
        }
    }
}
