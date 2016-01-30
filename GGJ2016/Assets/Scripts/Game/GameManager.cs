using UnityEngine;using System.Collections;public class GameManager : MonoBehaviour {    public static GameManager instance = null;

    public Mapa currentMap;
    public GameObject hero;
    public Transform spawnPosition;
    public FollowTarget camera;    void Awake()    {        //if (instance == null)        {            instance = this;
            hero = (GameObject)Instantiate(hero, spawnPosition.position, Quaternion.identity);            //    DontDestroyOnLoad(instance);        }        /* else if (instance != this)         {             Destroy(this.gameObject);         }*/    }	// Use this for initialization	void Start () {
        camera.target = hero.transform;	}		// Update is called once per frame	void Update () {
        	}

    public void changeMap(GameObject map, Transform playerPosition)
    {
        currentMap.gameObject.SetActive(false);
        currentMap = map.GetComponent<Mapa>();
        currentMap.gameObject.SetActive(true);

        hero.transform.position = playerPosition.position;
    }    public void spawnEnemy(Vector3 position)
    {

    }}