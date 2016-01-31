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

    [Tooltip("Sonido de arrastrarse")]
    public AudioClip m_onDrag;

    protected Vector3 forceDir;

    [Tooltip("Fuerza de cada empujon, velocidad")]
    [Range(0, 50)]
    public float pushForce;
    [Tooltip("Distancia que queremos que se mueva")]
    [Range(0, 100)]
    public float moveDistance;
    protected Vector3 destinationPosition;

    [Tooltip("Tiempo que bloquea al player después de empujar")]
    [Range(0, 5)]
    public float heroTimeBlocked = 0.5f;

    protected Rigidbody rigidbody;
    protected Vector3 originPos;
    protected bool firstTime;

    bool moving = false;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        originPos = transform.position;
	}

    void OnEnable()
    {
        if (!firstTime)
            transform.position = originPos;
    }

	// Update is called once per frame
	void Update () {
	    if (moving && Vector3.Distance(destinationPosition, transform.position) < 0.01f)
        {
            rigidbody.velocity = Vector3.zero;
        }
	}

    public void onEnter(DIR dir, Vector3 forceDir, Hero hero)
    {
        if (!this.enabled) return;
        this.forceDir = forceDir;
        hero.blockHero(heroTimeBlocked);
        moving = true;
        rigidbody.velocity = forceDir * pushForce;
        destinationPosition = transform.position + forceDir * moveDistance;
    }

    public void onExit(DIR dir)
    {
        
    }

    public void desactiveMovable()
    {
        rigidbody.velocity = Vector3.zero;
        this.enabled = false;
    }
}
