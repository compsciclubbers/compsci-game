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
    //private int moveSpeed = 1;
    //private bool hit = false;
    //private int count = 0;
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        //rb.AddForce(0, 0, 2000);
        rb.AddForce(transform.forward * 2000);
    }
    /*
    void OnCollisionEnter()
    {
        hit = true;
    }
        // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            //transform.position += transform.forward * moveSpeed;
            transform.position += transform.forward;
            print("moving" + hit);
        }

        else if (count < 10) 
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            count++;
            print("frozen" + hit);
        }
    }
    */
    

}
