using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Life))]
public class Enemy : MonoBehaviour {

	[Tooltip("Daño que hace este enemigo")]
	public float damage;
	[Tooltip("Tiempo de espera hasta nuevo ataque")]
	public float damageTimer;
	[Tooltip("Perseguidor (true), disparo (false)")]
	public bool isSeeker;

	[Tooltip("Prefab del disparo")]
	public GameObject shoot;
	protected PoolManager shootsPool;
	[Tooltip("Donde nace el disparo")]
	public Transform shootSpawn;
	[Tooltip("Daño del disparo, por golpe")]
	public float shootDamage;

    protected Life life;

	protected Transform target;
	protected Transform myTransform;
	protected NavMeshAgent agent;
	protected float distance;
	protected float nextAttack = 0.0f;
	protected float currentAngle;

	protected Animator animator;

	void Awake()
	{
		if (!isSeeker) {
			shootsPool = new PoolManager (shoot, 10);
			shootsPool.Init ();
		}
		animator = gameObject.GetComponentInChildren<Animator>();
		currentAngle = 0f;
	}

	// Use this for initialization
	void Start () {
		myTransform = transform;
		target = GameManager.instance.hero.transform;
		agent = GetComponent<NavMeshAgent> ();

		// Disparos
		if (!isSeeker) {
			shootsPool.Reset ();
			agent.Stop ();
		} else {
			agent.updatePosition = true;
			agent.updateRotation = false;
		}

        life = gameObject.GetComponent<Life>();
        life.registerOnDead(onDead);
        life.registerOnDamage(onDamage);
	}

    public void Reset()
    {
		life.init();
		animator.SetBool ("restart", true);
		animator.SetBool ("dead", false);
    }
	
	// Update is called once per frame
	void Update () {
//		myTransform.LookAt(target);
		animator.SetFloat("speed", agent.velocity.sqrMagnitude);
	}

	void FixedUpdate() {
		nextAttack -= Time.fixedDeltaTime;

		float distX = (target.position.x - agent.transform.position.x);
		float distZ = (target.position.z - agent.transform.position.z);
		if (distX > 0.0f) {
			animator.SetBool ("left", false);
			if (distZ > 0.0f) {
				animator.SetBool ("down", false);
				if (distX >= distZ) {
					animator.SetBool ("right", true);
					animator.SetBool ("up", false);
				} else {
					animator.SetBool ("up", true);
					animator.SetBool ("right", false);
				}
			} else {
				animator.SetBool ("up", false);
				if (distX*distX >= distZ*distZ) {
					animator.SetBool ("right", true);
					animator.SetBool ("down", false);
				} else {
					animator.SetBool ("down", true);
					animator.SetBool ("right", false);
				}
			}
		} else {
			animator.SetBool ("right", false);
			if (distZ > 0.0f) {
				animator.SetBool ("down", false);
				if (distX*distX >= distZ*distZ) {
					animator.SetBool ("left", true);
					animator.SetBool ("up", false);
				} else {
					animator.SetBool ("up", true);
					animator.SetBool ("left", false);
				}
			} else {
				animator.SetBool ("up", false);
				if (distX <= distZ) {
					animator.SetBool ("left", true);
					animator.SetBool ("down", false);
				} else {
					animator.SetBool ("down", true);
					animator.SetBool ("left", false);
				}
			}
		}

		if (nextAttack < 0.0f) {
			animator.SetBool ("attack", true);
			findTarget ();
		} else {
			animator.SetBool ("attack", false);
			agent.destination = myTransform.position;
		}
	}

	void findTarget() {
		if (isSeeker) {
			distance = Vector3.Distance (target.position, myTransform.position);
			if (distance < 1.0f) {
				nextAttack = damageTimer;
				target.GetComponent<Life> ().OnDamage (damage);
			} else if (distance < 5.0f) {
				agent.destination = target.position;
			}
		} else {
			attackTarget ();
		}
	}

	void attackTarget() {
		nextAttack = damageTimer;
		// Disparar
//		Vector2 shootTarget = new Vector2(target.position.x, target.position.z);
//		shootTarget.Normalize ();
//		GameObject shootAux = shootsPool.getObject(false);
//		shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);

		Vector3 targetDir = target.position - myTransform.position;
		// Devolvemos el spawn a su sitio inicial
		shootSpawn.RotateAround(myTransform.position, Vector3.up, -currentAngle);
		float shotAngle = Vector3.Angle(myTransform.forward, targetDir);
		Debug.Log ("shotAngle " + shotAngle);
		if (targetDir.x < 0f)
		{
			shotAngle *= -1;
		}
		currentAngle = shotAngle;
		shootSpawn.RotateAround(myTransform.position, Vector3.up, shotAngle);
		GameObject shootAux = shootsPool.getObject(false);
		shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);
	}
    public void onDamage(float currentLif)
	{
		Debug.Log ("Enemy local onDamage!!");
//		animator.SetBool("damage", true);
    }
    public void onDead()
    {
		gameObject.SetActive(false);
		animator.SetBool ("dead", true);
    }

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Enemy local Collision!!");
	}
}
