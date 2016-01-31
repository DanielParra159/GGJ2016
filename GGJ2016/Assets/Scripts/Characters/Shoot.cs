using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    [Tooltip("Sonido de colisionar con muro")]
    public AudioClip m_onCollision;

    [Tooltip("Velocidad de movimiento")]
    [Range(0, 100)]
    public float movSpeed = 0.5f;

    protected Vector3 originPos;
    protected Vector3 direction;
    protected Pausable pausable;
    protected Transform myTransform;
    protected Rigidbody rigidbody;
    protected float damage;

    protected GameObject owner;
	// Use this for initialization
	void Awake () {
        myTransform = transform;
        rigidbody = GetComponent<Rigidbody>();
        pausable = new Pausable(onPause, onResume);
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (pausable.Check()) return;

        //Debug.Log(myTransform.forward);
        myTransform.Translate(direction * movSpeed);
	}

    public void Spawn(Vector3 position, Quaternion rotation, float damage, GameObject owner)
    {
        myTransform.position = position;
        originPos = position;

        myTransform.rotation = rotation;
        this.damage = damage;
        //float angle = rotation.eulerAngles.y  * Mathf.Deg2Rad;
        //direction = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)).normalized;
        rigidbody.velocity = myTransform.transform.forward * movSpeed;
        //direction = rotation.eulerAngles;
        Debug.Log(direction);
        gameObject.SetActive(true);
        this.owner = owner;

        if (GetComponentInChildren<Animator>() != null)
            gameObject.GetComponentInChildren<Animator>().SetTrigger("shoot");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (owner == collision.gameObject) return;
        Debug.Log("Collision");
        if (collision.gameObject.tag.Equals("Wall"))
        {
            if (m_onCollision != null)
            {
                SoundManager.instance.PlaySingle(m_onCollision);
            }
        }
        else  if (collision.gameObject.tag.Equals("ColorWall"))
        {
            collision.gameObject.GetComponent<ColorWall>().collision(Vector3.Distance(originPos, collision.transform.position));
        }
        else //enemy || player || boss
        {
            Life life = collision.gameObject.GetComponent<Life>();
            if (life == null) return;
            collision.gameObject.GetComponent<Life>().OnDamage(damage);
        }

        gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (owner == other.gameObject) return;
        {
            Life life = other.gameObject.GetComponent<Life>();
            if (life == null) return;
            other.gameObject.GetComponent<Life>().OnDamage(damage);
        }
    }
    public void onPause()
    {
        rigidbody.velocity = Vector3.zero;
    }
    public void onResume()
    {
        rigidbody.velocity = myTransform.transform.forward * movSpeed;
    }
}
