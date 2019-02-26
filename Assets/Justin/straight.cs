using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickyObject : MonoBehaviour
{ 
    void OnCollisionEnter(Collision c)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        
        //rb.detectCollisions = false;
        //var joint = gameObject.AddComponent<FixedJoint>();
        //joint.connectedBody = c.rigidbody;
    }
}

public class straight : StickyObject
{
    private float startx;
    private float starty;
    private float startz;
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        startx = rb.transform.position.x;
        starty = rb.transform.position.y;
        startz = rb.transform.position.z;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        //rb.AddForce(0, 0, 2000);
        rb.AddForce(transform.forward * 2000);
    }
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float x = rb.transform.position.x;
        float y = rb.transform.position.y;
        float z = rb.transform.position.z;
        if(Math.Abs(x - startx) + Math.Abs(y - starty) + Math.Abs(z - startz) > 800)
        {
            Destroy(gameObject);
        }
    }


}
