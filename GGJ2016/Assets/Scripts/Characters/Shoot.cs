using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    [Tooltip("Sonido de colisionar con muro")]
    public AudioClip m_onCollision;

    [Tooltip("Velocidad de movimiento")]
    [Range(0, 100)]
    public float movSpeed = 0.5f;

    protected Vector3 direction;
    protected Pausable pausable;
    protected Transform myTransform;
    protected float damage;

    protected GameObject owner;
	// Use this for initialization
	void Awake () {
        myTransform = transform;
        pausable = new Pausable(onPause, onResume);
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (pausable.Check()) return;

        myTransform.Translate(direction * movSpeed);
	}

    public void Spawn(Vector3 position, Vector3 dir, float damage, GameObject owner)
    {
        myTransform.position = position;
        myTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        this.damage = damage;
        direction.x = dir.x;
        direction.y = dir.y;
        gameObject.SetActive(true);
        this.owner = owner;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (owner == collision.gameObject) return;
        if (collision.gameObject.tag.Equals("Wall") && m_onCollision != null)
        {
            SoundManager.instance.PlaySingle(m_onCollision);
        }
        else //enemy || player
        {
            collision.gameObject.GetComponent<Life>().OnDamage(damage);
        }

        gameObject.SetActive(false);
    }
    public void onPause()
    {

    }
    public void onResume()
    {

    }
}
