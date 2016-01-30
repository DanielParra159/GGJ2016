using UnityEngine;
using System.Collections;

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

	Transform target;
	Transform myTransform;
	NavMeshAgent agent;
	float distance;
	float nextAttack = 0;

	void Awake()
	{
		if (!isSeeker) {
			shootsPool = new PoolManager (shoot, 10);
			shootsPool.Init ();
		}
	}

	// Use this for initialization
	void Start () {
		myTransform = transform;
		target = GameManager.instance.hero.transform;
		agent = GetComponent<NavMeshAgent> ();
		// Disparos
		if (!isSeeker) {
			shootsPool.Reset();
		}
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.LookAt(target);
		
	}

	void FixedUpdate() {
		nextAttack -= Time.fixedDeltaTime;
		if (nextAttack < 0.0f) {
			findTarget ();
		} else {
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
}
