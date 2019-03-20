using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMon_Ani_Test : MonoBehaviour {

	public const string IDLE	= "Idle";
	public const string RUN		= "Run";
	public const string ATTACK	= "Attack";
	public const string DAMAGE	= "Damage";
	public const string DEATH	= "Death";
    [Header("AI settings")]
    public GameObject target;
    public float Speed;
    public float distanceUntilChase;

    Animation anim;

	void Start () {
		anim = GetComponent<Animation>();
        Speed *= Time.deltaTime;
	}

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
        transform.LookAt(target.transform);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AttackAni();
        }
        else if(collision.gameObject.tag != "Ground")
        {
            GetComponent<Rigidbody>().AddForce(0, 200, 0);
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
	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}
}