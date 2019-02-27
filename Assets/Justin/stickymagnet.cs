using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickymagnet : MonoBehaviour
{
    public float gravityScale = 1f;
    public static float globalGravity = -25f;
    private Rigidbody rb;
    private bool have = false;
    private GameObject holder;
    public GameObject camera;
    private bool tpressed = false;
    // Start is called before the first frame update
    void Start()
    {
        setGravity();
        GetComponent<Rigidbody>().isKinematic = true ;
        GetComponent<SphereCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey("z"))
        {
            transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles + new Vector3(-90, 0, -90));
            transform.position = holder.transform.position + new Vector3((float).5, (float).5, 0);
        }
        */
        //else
        //{
        if(tpressed)
            useGravity();
        //}
        if (Input.GetKey("t"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(0, 200, 150);
            GetComponent<SphereCollider>().isTrigger = false;
            tpressed = true;
            /*
            if (Input.GetKey("z"))
            {
                Vector3 temp = camera.transform.forward * 2500;
                rb.AddForce(temp);
            }
            else
            {
                              
            }
            have = false;
            */

        }
    }

    void setGravity()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void useGravity()
    {
        Vector3 g = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(g, ForceMode.Acceleration);
    }
    
    void OnCollisionEnter(Collision c)
    {
        /*
        if (!have && c.gameObject.tag == "Player") 
        {
            Destroy(rb.GetComponent<FixedJoint>());
            holder = c.gameObject;
            Vector3 pos = c.transform.position;
            Vector3 curob = transform.position;
            curob.x = pos.x + .5f;
            curob.y = pos.y + .5f;
            curob.z = pos.z;
            transform.position = curob;
            //transform.rotation = holder.transform.rotation;
            have = true;
        }
        else
        {
         */
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = c.rigidbody;
    }
   
    }
