using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour {

    [Tooltip("Llave que necesita")]
    public Collectable.COLLECTABLES key = Collectable.COLLECTABLES.NONE;

    [Tooltip("Esta abierta?")]
    public bool opened;

    protected BoxCollider collider;
    public bool chronometer;
    public float chronometerTime = 10.0f;
    protected float currentChronometerTime = 10.0f;

    private bool isInverted = false;

    public AudioClip m_onOpen;
    public AudioClip m_onClose;

    void Start()
    {


        if(isInverted)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        collider = gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponent<Animator>().SetBool("Open",opened);

        if (opened){
            gameObject.GetComponent<Animator>().Play("AlreadyOpenHor");
            gameObject.GetComponent<Animator>().Play("AlreadyOpenVer");
            collider.enabled = false;

                
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("AlreadyCloseHor");
            gameObject.GetComponent<Animator>().Play("AlreadyCloseVer");
            collider.enabled = true;
            
        }

        
    }
	// Use this for initialization
    void OnEnable()
    {
        if (isInverted)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        collider = gameObject.GetComponent<BoxCollider>();

        gameObject.GetComponent<Animator>().SetBool("Open", opened);

        if (opened)
        {
            gameObject.GetComponent<Animator>().Play("AlreadyOpenHor");
            gameObject.GetComponent<Animator>().Play("AlreadyOpenVer");
            collider.enabled = false;

        }
        else
        {
            gameObject.GetComponent<Animator>().Play("AlreadyCloseHor");
            gameObject.GetComponent<Animator>().Play("AlreadyCloseVer");
            collider.enabled = true;

        }
        if (chronometer)
        {
            this.open();
            currentChronometerTime = chronometerTime;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (chronometer)
        {
            currentChronometerTime -= Time.deltaTime;
            if (currentChronometerTime < 0.0f)
            {
                this.close();
            }
        }
	}

    public void open()
    {
        opened = true;
        collider.enabled = false;
        gameObject.GetComponent<Animator>().Play("OpenDoorVer");
        gameObject.GetComponent<Animator>().Play("OpenDoorHor");
       // gameObject.GetComponent<Animator>().SetBool("Open",true);
        if (m_onOpen != null)
        {
            SoundManager.instance.PlaySingle(m_onOpen);
        }
    }
    public void close()
    {
        opened = false;
        collider.enabled = true;
        gameObject.GetComponent<Animator>().Play("CloseDoorHor");
        gameObject.GetComponent<Animator>().Play("CloseDoorVer");
        //gameObject.GetComponent<Animator>().SetBool("Open", false);


        if (m_onClose != null)
        {
            SoundManager.instance.PlaySingle(m_onClose);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!opened && key == Collectable.COLLECTABLES.NONE || other.GetComponent<Hero>().haveItem(key))
        {
            open();
        }
    }
}
