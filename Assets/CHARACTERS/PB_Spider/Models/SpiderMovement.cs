using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpiderMovement : DamagableEntity
{
    [Header("AI settings")]
    public GameObject target;
    public float Speed;
    public float distanceUntilChase;
    private double startTime;
    public float gravityScale = 4f;
    public float globalGravity = -9.8f;
    private double spawnTimer;
    public GameObject small;
    public float offset;
    private Vector3 offsetTarget;
    private double offTime;
    public SpiderMovement() : this(15, 2, "Spider")
    {}

    public SpiderMovement(int hp, int dmg, string type): base(hp, dmg, type)
    {
        startTime = Time.time;
        offTime = Time.time;
    }


    private void Start()
    {
        spawnTimer = Time.time;
    }
    void Update()
    {
        double now = Time.time;
        if(now - spawnTimer > 20)
        {
            GameObject littleboy = Instantiate(small, transform.position + transform.forward * 6, Quaternion.identity);
            littleboy.SetActive(true);
            littleboy.GetComponent<Smallspider>().wave = false;
            littleboy = Instantiate(small, transform.position - transform.forward * 6, Quaternion.identity);
            littleboy.SetActive(true);
            littleboy.GetComponent<Smallspider>().wave = false;
            spawnTimer = now;
        }
        // if ((target.transform.position - this.transform.position).sqrMagnitude < distanceUntilChase)
        if (Time.time - offTime > 2)
        {
            setOffset();
            offTime = Time.time;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        transform.LookAt(offsetTarget);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        if (getHit())
        {
            //renderer.material.color = new Color(255, 0, 0);
            knockBack();
            setHit(false);
        }
        useGravity();
        if (getDead())
        {
            DestroyImmediate(gameObject);
            GameObject.FindGameObjectWithTag("WaveCheck").GetComponent<Wavemanager>().numberOfEnemies--;
            //gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        double now = Time.time;
        if (collision.gameObject.tag == "Player" && now - startTime > 0.5)
        {
            knockBack();
            dealDamage(collision.gameObject);
            startTime = Time.time;
        }
      
        else if (collision.gameObject.tag != "Ground" && now - startTime > 0.1 && collision.gameObject.transform.position.y + 1 > transform.position.y)
        {
            jumpOverObstacle();
            startTime = Time.time;
        }
        updateHealth();
        
    }

    private void jumpOverObstacle()
    {
        GetComponent<Rigidbody>().AddForce(0, 400, 0);
    }
    private void knockBack()
    {
        GetComponent<Rigidbody>().AddForce(0, 100, 0);
        GetComponent<Rigidbody>().AddForce(transform.forward * -800);
    }
    void useGravity()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 g = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(g, ForceMode.Acceleration);
    }
    private void setOffset()
    {
        float offsetx = Random.Range(target.transform.position.x - offset, target.transform.position.x + offset);
        float offsetz = Random.Range(target.transform.position.z - offset, target.transform.position.z + offset);
        offsetTarget = new Vector3(offsetx, target.transform.position.y, offsetz);
    }
}
