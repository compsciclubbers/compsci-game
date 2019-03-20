using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : DamagableEntity
{
    [Header("AI settings")]
    public GameObject target;
    public float Speed;
    public float distanceUntilChase;
    private Collider flag;
    private double startTime;
    public float gravityScale = 4f;
    public float globalGravity = -9.8f;
    public SpiderMovement() : this(10, 2, "Spider")
    {}

    public SpiderMovement(int hp, int dmg, string type): base(hp, dmg, type)
    {
        flag = GetComponent<Collider>();
        startTime = Time.time;
    }


    void Update()
    {
        // if ((target.transform.position - this.transform.position).sqrMagnitude < distanceUntilChase)
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
        useGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        double now = Time.time;
        if (collision.gameObject.tag == "Player" && now - startTime > 0.5)
        {
            //knockBack();
            dealDamage(collision.gameObject);
            startTime = Time.time;
        }
      
        else if (collision.gameObject.tag != "Ground" && now - startTime > 0.5 && collision.gameObject.transform.position.y + 2 > transform.position.y)
        {
            jumpOverObstacle();
            startTime = Time.time;
        }
        
        
    }

    private void jumpOverObstacle()
    {
        GetComponent<Rigidbody>().AddForce(0, 1200, 0);
    }
    private void knockBack()
    {
        GetComponent<Rigidbody>().AddForce(0, 250, 0);
        GetComponent<Rigidbody>().AddForce(transform.forward * -600);
    }
    void useGravity()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 g = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(g, ForceMode.Acceleration);
    }
}