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

	Transform target;
	Transform myTransform;
	NavMeshAgent agent;
	float distance;
	float nextAttack = 0;

	protected Animator animator;

	void Awake()
	{
		if (!isSeeker) {
			shootsPool = new PoolManager (shoot, 10);
			shootsPool.Init ();
		}
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	// Use this for initialization
	void Start () {
		myTransform = transform;
		target = GameManager.instance.hero.transform;
		agent = GetComponent<NavMeshAgent> ();
		// Disparos
		if (!isSeeker) {
			shootsPool.Reset();
			agent.Stop ();
		}

        life = gameObject.GetComponent<Life>();
        life.registerOnDead(onDead);
        life.registerOnDamage(onDamage);
	}

    public void Reset()
    {
		life.init();
		animator.SetTrigger("restart");
    }
	
	// Update is called once per frame
	void Update () {
//		myTransform.LookAt(target);

		animator.SetFloat("speed", agent.velocity.sqrMagnitude);

		if (isSeeker) {
			if (agent.velocity.x > 0.0f) {
				animator.SetBool ("right", true);
				animator.SetBool ("left", false);
			} else {
				animator.SetBool ("left", true);
			}

			if (agent.velocity.z > 0.0f) {
				animator.SetBool ("up", true);
			} else {
				animator.SetBool ("down", true);
			}
		} else {
			float distX = (target.position.x - agent.transform.position.x);
			float distZ = (target.position.z - agent.transform.position.z);
			if (distX > 0.0f) {
				animator.SetBool ("left", false);
				if (distZ > 0.0f) {
					animator.SetBool ("down", false);
					if (distX*distX >= distZ*distZ) {
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
				Debug.Log ("X " + distX*distX + " --  Z " + distZ*distZ);
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
					if (distX*distX >= distZ*distZ) {
						animator.SetBool ("left", true);
						animator.SetBool ("down", false);
					} else {
						animator.SetBool ("down", true);
						animator.SetBool ("left", false);
					}
				}
			}
			if (distZ > 0.0f) {
				animator.SetBool ("up", true);
			} else {
				animator.SetBool ("down", true);
			}
		}
	}

	void FixedUpdate() {
		nextAttack -= Time.fixedDeltaTime;
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
				Debug.Log ("Hit!");
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
		Vector2 shootTarget = new Vector2(target.position.x, target.position.z);
		shootTarget.Normalize ();
		GameObject shootAux = shootsPool.getObject(false);
		shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);
	}
    public void onDamage(float currentLif)
    {

    }
    public void onDead()
    {
        gameObject.SetActive(false);
    }
}
