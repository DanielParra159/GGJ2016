using UnityEngine;using System.Collections;
using System.Collections.Generic;[RequireComponent(typeof(Life))]//[RequireComponent(typeof(Attack))][RequireComponent(typeof(CharacterController))]public class Hero : MonoBehaviour {    [Tooltip("Velocidad de movimiento")]    [Range(0, 100)]    public float movSpeed = 10;    protected float currentMovSpeed = 10;    [Tooltip("Cadencia disparo, en segundos")]    [Range(0, 100)]    public float shootRate = 0.2f;    protected float timeToNextShoot;    [Tooltip("Daño del disparo, por golpe")]    [Range(1, 100)]    public float shootDamage = 1;    [Tooltip("Prefab del disparo")]    public GameObject shoot;    protected PoolManager shootsPool;    [Tooltip("Donde nace el disparo")]    public Transform shootSpawn;

    protected float timeBlocked;

    protected List<Collectable.COLLECTABLES> collectable;    protected Transform myTransform;    protected CharacterController characterController;    protected Pausable pausable;    protected Life life;

    protected Vector2 movAxis;    void Awake()    {        shootsPool = new PoolManager(shoot, 10);        shootsPool.Init();

        collectable = new List<Collectable.COLLECTABLES>();    }	// Use this for initialization	void Start () {        myTransform = transform;        characterController = gameObject.GetComponent<CharacterController>();        pausable = new Pausable(onPause, onResume);        shootsPool.Reset();        life = gameObject.GetComponent<Life>();        life.registerOnDead(onDead);        life.registerOnDamage(onDamage);                    currentMovSpeed = movSpeed;	}		// Update is called once per frame	void Update () {        if (pausable.Check()) return;    }    void FixedUpdate()    {        if (pausable.Check()) return;
        timeBlocked -= Time.fixedDeltaTime;
        if (timeBlocked > 0.0f) return;        movAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.One);        if (movAxis == Vector2.zero)        {            movAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.KeyboardL, GamepadInput.GamePad.Index.One);        }        Vector2 shootAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.RightStick, GamepadInput.GamePad.Index.One);        if (shootAxis == Vector2.zero)        {            shootAxis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.KeyboardR, GamepadInput.GamePad.Index.One);        }
        timeToNextShoot -= Time.fixedDeltaTime;        if (timeToNextShoot < 0.0f && shootAxis != Vector2.zero)        {            timeToNextShoot = shootRate;            GameObject shootAux = shootsPool.getObject(false);
            Vector3 dir = (Vector3.right * shootAxis.x + Vector3.forward * shootAxis.y);
            shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);        }        if (movAxis != Vector2.zero)        {            characterController.Move(new Vector3(movAxis.x, 0.0f, movAxis.y) * Time.fixedDeltaTime * movSpeed);        }        Vector3 lookDir;        if (shootAxis == Vector2.zero)        {            lookDir = (Vector3.right * movAxis.x + Vector3.forward * movAxis.y);        }        else        {            lookDir = (Vector3.right * shootAxis.x + Vector3.forward * shootAxis.y);        }        if (lookDir != Vector3.zero)        {            myTransform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);        }    }
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
    }    public void onDamage(float currentLif)    {            }    public void onDead()    {            }    public void onPause()    {    }    public void onResume()    {    }    public Vector2 getMovAxis()
    {
        return movAxis;
    }    public void blockHero(float time)
    {
        timeBlocked = time;
        //characterController. = Vector3.zero;
    }}