using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void NotifyOnDead();
public delegate void NotifyOnDamage(float currentLif);

public class Life : MonoBehaviour
{
    [Tooltip("Vida maxima")]
    [Range(1, 1000)]
    public float m_maxLife = 100.0f;
    [HideInInspector]
    private float m_currentLife;
    [Tooltip("Regeneración por segundo")]
    [Range(0, 1000)]
    public float m_regeneration= 0.0f;
    [Tooltip("Regeneración por segundo")]

    public AudioClip m_onDamageSound;
    public AudioClip m_onDeadSound;


    private NotifyOnDead m_onDead = null;
    private NotifyOnDamage m_onDamage = null;


    protected Pausable m_pausable;
    // Use this for initialization
    void Start()
    {
        init();
        m_pausable = new Pausable();
    }
    public void init()
    {
        m_currentLife = m_maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_pausable.Check()) return;
        m_currentLife += m_regeneration * Time.deltaTime * TimeManager.currentTimeFactor;
        if ( m_currentLife > m_maxLife)
        {
            m_currentLife = m_maxLife;
            //this.enabled = false;
        }
    }
    public bool OnDamage(float damage)
    {
        bool dead = false;
        this.enabled = true;
        m_currentLife -= damage;
        if (m_currentLife <= 0.0f)
        {
            if (m_onDead != null)
                m_onDead();
            m_currentLife = 0.0f;
            dead = true;

            if (m_onDeadSound != null)
            {
                SoundManager.instance.PlaySingle(m_onDeadSound);
            }
        }
        else if (m_onDamage != null)
        {
            if (m_onDamageSound != null)
            {
                SoundManager.instance.PlaySingle(m_onDamageSound);
            }
            m_onDamage(m_currentLife);
        }

        return dead;
    }
    public bool isAlive()
    {
        return m_currentLife > 0.0f;
    }

    public float getLife()
    {
        return m_currentLife;
    }

    public float getMaxLife()
    {
        return m_maxLife;
    }

    /*
     * Registra la funcion que ser� llamada cuando se quede sin vida
     */
    public void registerOnDead( NotifyOnDead onDead)
    {
        m_onDead += onDead;
    }
    public void unregisterOnDead(NotifyOnDead onDead)
    {
        m_onDead -= onDead;
    }

    /*
     * Registra la funcion que ser� llamada cuando reciba un da�o y no se muera
     */
    public void registerOnDamage(NotifyOnDamage onDamage)
    {
        m_onDamage += onDamage;
    }
    public void unregisterOnDamage(NotifyOnDamage onDamage)
    {
        m_onDamage -= onDamage;
    }

    public void setRegeneration(float regeneration)
    {
        m_regeneration = regeneration;
    }
}
