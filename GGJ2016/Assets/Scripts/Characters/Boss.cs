using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Life))]
public class Boss : MonoBehaviour {

    [Tooltip("Cadencia disparo, en segundos")]
    [Range(0, 100)]
    public float shootRate = 0.2f;
    protected float timeToNextShoot;
    [Tooltip("Daño del disparo, por golpe")]
    [Range(1, 100)]
    public float shootDamage = 1;

    [Tooltip("Prefab del disparo")]
    public GameObject shoot;
    protected PoolManager shootsPool;
    [Tooltip("Donde nace el disparo")]
    public Transform shootSpawn;
    protected Life life;

    protected Transform myTransform;
    protected float currentAngle;
    protected GameObject hero;
    protected bool unit = false;

    void Awake()
    {
        myTransform = transform;
        shootsPool = new PoolManager(shoot, 10);
        shootsPool.Init();
        currentAngle = 0.0f;
    }
	// Use this for initialization
	void Start () {
        shootsPool.Reset();

        life = gameObject.GetComponent<Life>();
        life.registerOnDead(onDead);
        life.registerOnDamage(onDamage);
        hero = GameManager.instance.hero;

        unit = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!unit) return;
        Vector3 dir = hero.transform.position - myTransform.position;
        dir.Normalize();
        dir.y = 0.0f;
        Vector3 lookDir = (Vector3.right * dir.x + Vector3.forward * dir.z);

	    timeToNextShoot -= Time.fixedDeltaTime;
        if (timeToNextShoot < 0.0f)
        {
            /*Vector3 targetDir = hero.transform.position - myTransform.position;
            // Devolvemos el spawn a su sitio inicial
            shootSpawn.RotateAround(myTransform.position, Vector3.up, -currentAngle);
            float shotAngle = Vector3.Angle(myTransform.forward, targetDir);
            Debug.Log("shotAngle " + shotAngle);
            if (targetDir.x < 0f)
            {
                shotAngle *= -1;
            }
            currentAngle = shotAngle;
            shootSpawn.RotateAround(myTransform.position, Vector3.up, shotAngle);
            GameObject shootAux = shootsPool.getObject(false);
            shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, shootSpawn.rotation, shootDamage, this.gameObject);
            */


            
            timeToNextShoot = shootRate;
            shootSpawn.RotateAround(myTransform.position, Vector3.up, -currentAngle);
            float shotAngle = Vector3.Angle(lookDir, new Vector3(0f, 0f, 1f));
            if (lookDir.x == 0.0f)
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
            //            Vector3 dir = (Vector3.right * shootAxis.x + Vector3.forward * shootAxis.y);
            shootAux.GetComponent<Shoot>().Spawn(shootSpawn.position, Quaternion.FromToRotation(Vector3.right, dir), shootDamage, this.gameObject);
        }
	}

    public void onDamage(float currentLif)
    {

    }
    public void onDead()
    {

    }
}
