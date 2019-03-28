using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMon_Ani_Test : DamagableEntity {
    private AudioSource takeDamage;

	public const string IDLE	= "Idle";
	public const string RUN		= "Run";
	public const string ATTACK	= "Attack";
	public const string DAMAGE	= "Damage";
	public const string DEATH	= "Death";
    [Header("AI settings")]
    public GameObject target;
    public float Speed = 4;
    public float distanceUntilChase;
    private double startTime;
    Animation anim;
    private double attLength;
    private bool justAttacked;
    private GameObject attackedObj;
    public int offset;
    public MushroomMon_Ani_Test() : this(8, 2, "shroom")
    {

    }

    public MushroomMon_Ani_Test(int hp, int dmg, string type) : base(hp, dmg, type)
    {
        attLength = 0;
        justAttacked = false;
    }
	void Start () {
        takeDamage = GetComponent<AudioSource>();

        startTime = Time.time;
        anim = GetComponent<Animation>();
	}

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        transform.LookAt(target.transform);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        if (getHit())
        {
            takeDamage.Play();

            knockBack();
            setHit(false);  
        }
        if (attLength <= Time.time && justAttacked)
        {
            knockBack();
            dealDamage(attackedObj);
            justAttacked = false;
        }
        updateHealth();
        if (getDead())
        {
            DeathAni();
            //gameObject.SetActive(false);
            DestroyImmediate(gameObject);
            GameObject.FindGameObjectWithTag("WaveCheck").GetComponent<Wavemanager>().numberOfEnemies--;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        double now = Time.time;
        if (collision.gameObject.tag == "Player" && now - startTime > 0.3)
        {
            attackedObj = collision.gameObject;
            AttackAni();
            startTime = Time.time;
        }
        else if(collision.gameObject.tag != "Ground" && now - startTime > 0.1)
        {
            GetComponent<Rigidbody>().AddForce(0, 200, 0);
            startTime = Time.time;
        }
    }

    public void IdleAni (){
		anim.CrossFade (IDLE);
	}

	public void RunAni (){
		anim.CrossFade (RUN);
	}

	public void AttackAni (){
		anim.CrossFade (ATTACK);
        attLength = anim["Attack"].length + Time.time;
        justAttacked = true;
	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}
    public void knockBack()
    {
        GetComponent<Rigidbody>().AddForce(0, 100, 0);
        GetComponent<Rigidbody>().AddForce(transform.forward * -400);
    }
}