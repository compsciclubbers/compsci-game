using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cactusgun : MonoBehaviour
{
    private double startTime;
    private double curr;
    public GameObject shot;
    private GameObject holder;
    public GameObject camera;
    private bool have = false;
    private const int maxAmmo = 7;
    private int shotsFired;
    private bool zpressed = false;
    private Vector3 startPoint;
    private Vector3 forwardRotation;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time - .5;
        shotsFired = 0;
        startPoint = transform.position;
        forwardRotation = new Vector3(-90, 0, -90);

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        curr = Time.time - startTime;
        if (Input.GetKey("g") && curr > .5)
        {
                Vector3 gpos = transform.position;
                Vector3 gdir = transform.right;
                Quaternion grot = transform.rotation * Quaternion.Euler(new Vector3 (90,90,270));
                float spawnDistance = 3;
                Vector3 spawnp = gpos + gdir * spawnDistance;
                Instantiate(shot, spawnp, grot);
                startTime = Time.time;
                shotsFired++;
        }
        /*
        if(shotsFired >= maxAmmo)
        {
            Instantiate(gameObject, startPoint, Quaternion.Euler(forwardRotation));
            Destroy(gameObject);
        }
        */
    }
    /*
    void OnCollisionEnter(Collision c)
    {
        if (!have)
        {
            holder = c.gameObject;
            Vector3 pos = c.transform.position;
            Vector3 curob = transform.position;
            curob.x = pos.x + .5f;
            curob.y = pos.y + .5f;
            curob.z = pos.z - .5f;
            transform.position = curob;
            //transform.rotation = Quaternion.Euler(holder.transform.rotation.eulerAngles + new Vector3(90, 0, 90));
            have = true;
            BoxCollider bc = gameObject.GetComponent<BoxCollider>();
            bc.isTrigger = true;
        }
    }
   */
}
