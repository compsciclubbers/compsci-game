using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smallspider : DamagableEntity
{
    [Header("AI settings")]
    public GameObject target;
    public float Speed;
    public float distanceUntilChase;
    private Collider flag;
    private double startTime;
    public float gravityScale = 4f;
    public float globalGravity = -9.8f;
    public bool wave;
    public float offset;
    private Vector3 offsetTarget;
    private double offTime;
    public Smallspider() : this(3, 1, "Spider")
    { }

    public Smallspider(int hp, int dmg, string type) : base(hp, dmg, type)
    {
        wave = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        offTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // if ((target.transform.position - this.transform.position).sqrMagnitude < distanceUntilChase)
        if(Time.time - offTime > .3)
        {
            setOffset();
            offTime = Time.time;
        }
        transform.position = Vector3.MoveTowards(transform.position, offsetTarget, Speed * Time.deltaTime);
        transform.LookAt(offsetTarget);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        updateHealth();
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
            //gameObject.SetActive(false);  
            if (wave)
            {
                GameObject.FindGameObjectWithTag("WaveCheck").GetComponent<Wavemanager>().numberOfEnemies--;
            }
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

        else if (collision.gameObject.tag != "Ground" && now - startTime > 0.5 && collision.gameObject.transform.position.y + 1 > transform.position.y)
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
