using UnityEngine;
using System.Collections;

public class MovableTrigger : MonoBehaviour
{

    [Tooltip("El padre, xe!")]
    public Movable movableParent;

    [Tooltip("Direccion por la que debe empujar")]
    public Movable.DIR dir;
    
    [Tooltip("Segundos necesarios para empujar")]
    [Range(0, 10)]
    public float timePushingNecessary;
    protected float currentTimePushing;

    protected bool enter = false;
    protected Vector3 forceDir;
    protected Vector3 origPos;

    
    protected Hero hero;

	// Use this for initialization
	void Start () {
        origPos = transform.position;
        if (dir == Movable.DIR.LEFT)
        {
            forceDir.x = -1;
        }
        else if (dir == Movable.DIR.RIGHT)
        {
            forceDir.x = +1;
        }
        else if (dir == Movable.DIR.UP)
        {
            forceDir.z = +1;
        }
        else if (dir == Movable.DIR.DOWN)
        {
            forceDir.z = -1;
        }
	}
	void OnEnable()
    {
        transform.position = origPos;
    }
	// Update is called once per frame
	void Update () {
        if (enter)
        {
            Vector2 dirAux = hero.getMovAxis();
            dirAux.x *= forceDir.x;
            dirAux.y *= forceDir.z;
            if (dirAux != Vector2.zero)
            {
                currentTimePushing += Time.deltaTime;
                if (currentTimePushing > timePushingNecessary)
                {
                    currentTimePushing = 0.0f;
                    movableParent.onEnter(dir, forceDir, hero);
                    enter = false;
                }
            }
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        hero = other.gameObject.GetComponent<Hero>();
        if (hero == null) return;
        enter = true;
        hero = other.gameObject.GetComponent<Hero>();
    }

    void OnTriggerExit(Collider other)
    {
        currentTimePushing = 0.0f;
        movableParent.onExit(dir);
    }
}
