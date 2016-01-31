using UnityEngine;using System.Collections;public class GameManager : MonoBehaviour {    public static GameManager instance = null;

    public enum GAME_STATES
    {
        STATE_GAME, STATE_DEATH, STATE_CHANGE_MAP
    }
    public GameObject circleTriggers;
    protected GAME_STATES currentState;
    public Mapa currentMap;
    protected Mapa initialMap;
    public GameObject hero;
    public Transform spawnPosition;
    [HideInInspector]
    public Transform origPosition;
    public FollowTarget camera;    void Awake()    {        //if (instance == null)        {            instance = this;
            hero = (GameObject)Instantiate(hero, spawnPosition.position, Quaternion.identity);
            origPosition = spawnPosition;
            circleTriggers = (GameObject)Instantiate(circleTriggers, spawnPosition.position, Quaternion.identity);
            circleTriggers.GetComponent<FollowTarget>().target = hero.transform;            //    DontDestroyOnLoad(instance);        }        /* else if (instance != this)         {             Destroy(this.gameObject);         }*/    }	// Use this for initialization	void Start () {
        camera.target = hero.transform;
        initialMap = currentMap;	}		// Update is called once per frame	void Update () {
        	}

    public void changeMap(GameObject map, Transform playerPosition)
    {
        currentMap.gameObject.SetActive(false);
        currentMap = map.GetComponent<Mapa>();
        currentMap.gameObject.SetActive(true);

        hero.transform.position = playerPosition.position;
    }    public void spawnEnemy(Vector3 position)
    {

    }    public void setState(GAME_STATES state)
    {
        currentState = state;
        switch(state)
        {
            case GAME_STATES.STATE_DEATH:
                changeMap(initialMap.gameObject, spawnPosition);
                hero.GetComponent<Hero>().Reset();
                break;
        }
    }}