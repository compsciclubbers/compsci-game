using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMon_Ani_Test : DamagableEntity {

	public const string IDLE	= "Idle";
	public const string RUN		= "Run";
	public const string ATTACK	= "Attack";
	public const string DAMAGE	= "Damage";
	public const string DEATH	= "Death";
    [Header("AI settings")]
    public GameObject target;
    private float Speed = 5;
    public float distanceUntilChase;
    private double startTime;
    Animation anim;
    private double attLength;
    private bool justAttacked;
    private GameObject attackedObj;
    public MushroomMon_Ani_Test() : this(6, 2, "shroom")
    {

    }

    public MushroomMon_Ani_Test(int hp, int dmg, string type) : base(hp, dmg, type)
    {
        attLength = 0;
        justAttacked = false;
    }
	void Start () {
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
            knockBack();
            setHit(false);  
        }
        if (getDead())
        {
            gameObject.SetActive(false);
        }
        if(attLength <= Time.time && justAttacked)
        {
            knockBack();
            dealDamage(attackedObj);
            justAttacked = false;
        }
        updateHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        double now = Time.time;
        if (collision.gameObject.tag == "Player" && now - startTime > 0.5)
        {
            attackedObj = collision.gameObject;
            AttackAni();
            startTime = Time.time;
        }
        else if(collision.gameObject.tag != "Ground" && now - startTime > 0.5)
        {
            GetComponent<Rigidbody>().AddForce(0, 400, 0);
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