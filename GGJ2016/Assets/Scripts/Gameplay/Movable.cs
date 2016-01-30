using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

    public enum DIR
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    //Dani: me sangran los ojos al ver esto...
    protected bool moving = false;

    [Tooltip("Sonido de arrastrarse")]
    public AudioClip m_onDrag;

    [Tooltip("Direccion por la que debe empujar")]
    public DIR dir;
    protected Vector3 forceDir;
    [Tooltip("Segundos necesarios para empujar")]
    [Range(0, 10)]
    public float timePushingNecessary;
    protected float currentTimePushing;
    protected bool enter = false;

    [Tooltip("Fuerza de cada empujon, velocidad")]
    [Range(0, 50)]
    public float pushForce;
    [Tooltip("Distancia que queremos que se mueva")]
    [Range(1, 100)]
    public float moveDistance;
    protected Vector3 destinationPosition;

    [Tooltip("Tiempo que bloquea al player después de empujar")]
    [Range(0, 5)]
    public float heroTimeBlocked = 0.5f;

    protected Rigidbody rigidbody;
    protected Transform parentTransform;

    protected Hero hero;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponentInParent<Rigidbody>();
        parentTransform = transform.parent.transform;
        forceDir = Vector3.zero;
        if (dir == DIR.DOWN)
        {
            forceDir.z = -1;
        }
        else if (dir == DIR.UP)
        {
            forceDir.z = +1;
        }
        else if (dir == DIR.LEFT)
        {
            forceDir.x = -1;
        }
        else if (dir == DIR.RIGHT)
        {
            forceDir.x = +1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (Vector3.Distance(parentTransform.position, destinationPosition) < 0.5f)
            {
                moving = false;
                parentTransform.position = destinationPosition;
            }
            else
            {
                parentTransform.Translate(forceDir * Time.fixedDeltaTime);
            }
        } 
        else if (Vector3.Distance(parentTransform.position, destinationPosition) > moveDistance)
        {
            rigidbody.velocity = Vector3.zero;
            moving = false;
            currentTimePushing = 0.0f;
        }
	    if (enter)
        {
            Vector2 vel = hero.getMovAxis();
            vel.x *= forceDir.x;
            vel.y *= forceDir.z;
            if (vel.sqrMagnitude > 0.0f)
            {
                currentTimePushing += Time.deltaTime;
            }
            if (currentTimePushing > timePushingNecessary)
            {
                Debug.Log(forceDir);
                currentTimePushing = 0.0f;
                rigidbody.velocity = (forceDir * pushForce);
                destinationPosition = parentTransform.position + forceDir*moveDistance;
                hero.blockHero(heroTimeBlocked);
                moving = true;
                if (m_onDrag != null)
                {
                    SoundManager.instance.PlaySingle(m_onDrag);
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        enter = true;
        hero = other.gameObject.GetComponent<Hero>();
    }

    void OnTriggerExit(Collider other)
    {
        enter = false;
    }
}
