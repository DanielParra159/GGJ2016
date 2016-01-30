using UnityEngine;using System.Collections;
using System.Collections.Generic;[RequireComponent(typeof(Life))][RequireComponent(typeof(CharacterController))]public class Hero : MonoBehaviour {    [Tooltip("Velocidad de movimiento")]    [Range(0, 100)]    public float movSpeed = 10;    protected float currentMovSpeed = 10;    [Tooltip("Cadencia disparo, en segundos")]    [Range(0, 100)]    public float shootRate = 0.2f;    protected float timeToNextShoot;    [Tooltip("Daño del disparo, por golpe")]    [Range(1, 100)]    public float shootDamage = 1;    [Tooltip("Prefab del disparo")]    public GameObject shoot;    protected PoolManager shootsPool;    [Tooltip("Donde nace el disparo")]    public Transform shootSpawn;

    protected float timeBlocked;

    protected List<Collectable.COLLECTABLES> collectable;    protected Transform myTransform;    protected CharacterController characterController;
    protected Animator animator;    protected Pausable pausable;    protected Life life;

    protected Vector2 movAxis;    protected float currentAngle;    void Awake()    {        shootsPool = new PoolManager(shoot, 10);        shootsPool.Init();

        collectable = new List<Collectable.COLLECTABLES>();        currentAngle = 0.0f;    }	// Use this for initialization	void Start () {        myTransform = transform;        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();        pausable = new Pausable(onPause, onResume);        shootsPool.Reset();        life = gameObject.GetComponent<Life>();        life.registerOnDead(onDead);        life.registerOnDamage(onDamage);

        Reset();	}	public void Reset()
    {
        currentMovSpeed = movSpeed;
        life.init();
        animator.SetTrigger("restart");
    }	// Update is called once per frame	void Update () {        if (pausable.Check()) return;    }    void FixedUpdate()    {        if (pausable.Check()) return;
        timeBlocked -= Time.fixedDeltaTime;
        if (timeBlocked > 0.0f) return;        movAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.One);        if (movAxis == Vector2.zero)        {            movAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.KeyboardL, GamepadInput.GamePad.Index.One);        }        Vector2 shootAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.RightStick, GamepadInput.GamePad.Index.One);        if (shootAxis == Vector2.zero)        {            shootAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.KeyboardR, GamepadInput.GamePad.Index.One);        }        if (movAxis != Vector2.zero)        {            characterController.Move(new Vector3(movAxis.x, 0.0f, movAxis.y) * Time.fixedDeltaTime * movSpeed);        }        Vector3 lookDir;        if (shootAxis == Vector2.zero)        {            lookDir = (Vector3.right * movAxis.x + Vector3.forward * movAxis.y);
            animator.SetFloat("speed", movAxis.SqrMagnitude());
            if (movAxis.x > 0)
            {
                animator.SetTrigger("right");
            }
            else if (movAxis.x < 0)
            {
                animator.SetTrigger("left");
            }
            else if (movAxis.y < 0)
            {
                animator.SetTrigger("down");
            }
            else if (movAxis.y > 0)
            {
                animator.SetTrigger("up");
            }        }        else        {            lookDir = (Vector3.right * shootAxis.x + Vector3.forward * shootAxis.y);        }        if (lookDir != Vector3.zero)        {
            //shootSpawn.rotation = Quaternion.LookRotation(lookDir, Vector3.up);            //myTransform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);        }

        timeToNextShoot -= Time.fixedDeltaTime;
        if (timeToNextShoot < 0.0f && shootAxis != Vector2.zero)
        {
            timeToNextShoot = shootRate;
            shootSpawn.RotateAround(myTransform.position, Vector3.up, -currentAngle);
            float shotAngle = Vector3.Angle(lookDir, new Vector3(0f, 0f, 1f));
            if (lookDir.x == 0f)
            {
                shotAngle *= lookDir.z;
            }
            else
            {
                shotAngle *= lookDir.x;
            }
            currentAngle = shotAngle;
            shootSpawn.RotateAround(myTransform.position, Vector3.up, shotAngle);
            GameObject shootAux = shootsPool.getObject(false);
            Vector3 dir = (Vector3.right * shootAxis.x + Vector3.forward * shootAxis.y);
            shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);


            animator.SetTrigger("attack");
            if (shootAxis.x > 0)
            {
                animator.SetTrigger("right");
            }
            else if (shootAxis.x < 0)
            {
                animator.SetTrigger("left");
            }
            else if (shootAxis.y < 0)
            {
                animator.SetTrigger("down");
            }
            else if (shootAxis.y > 0)
            {
                animator.SetTrigger("up");
            }
        }    }
    public void pickItem(Collectable.COLLECTABLES type)
    {
        collectable.Add(type);
    }
    public bool haveItem(Collectable.COLLECTABLES type)
    {
        return collectable.Contains(type);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, shootSpawn.position);
    }    public void onDamage(float currentLif)    {
        animator.SetTrigger("damage");    }    public void onDead()    {
        //me gustaría hacerlo por eventos, peroooo....
        GameManager.instance.setState(GameManager.GAME_STATES.STATE_DEATH);
        animator.SetTrigger("dead");    }    public void onPause()    {    }    public void onResume()    {    }    public Vector2 getMovAxis()
    {
        return movAxis;
    }    public void blockHero(float time)
    {
        timeBlocked = time;
        //characterController. = Vector3.zero;
    }}