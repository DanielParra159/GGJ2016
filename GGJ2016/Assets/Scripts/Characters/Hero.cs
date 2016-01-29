using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Life))]
//[RequireComponent(typeof(Attack))]
public class Hero : MonoBehaviour {

    [Tooltip("Velocidad de movimiento")]
    [Range(1, 100)]
    public float movSpeed = 10;
    protected float currentMovSpeed = 10;
    [Tooltip("Cadencia disparo, en segundos")]
    [Range(0, 100)]
    public float shootRate = 0.2f;
    [Tooltip("Daño del disparo, por golpe")]
    [Range(1, 100)]
    public float shootDamage = 1;

    protected Transform m_transform;
    protected Pausable m_pausable;

    protected Life m_life;

	// Use this for initialization
	void Start () {
        m_transform = transform;
        m_pausable = new Pausable(onPause, onResume);

        m_life = gameObject.GetComponent<Life>();
        m_life.registerOnDead(onDead);
        m_life.registerOnDamage(onDamage);
            
        currentMovSpeed = movSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_pausable.Check()) return;
    }
    public void onDamage(float currentLif)
    {
        
    }
    public void onDead()
    {
        
    }
    public void onPause()
    {

    }
    public void onResume()
    {

    }
}
