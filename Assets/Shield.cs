using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }
    
    void OnCollisionEnter(Collision collision)
    {
        print("Collision ahhhh");
        if (collision.gameObject.tag == "Player")
        {
            print("cp");
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<BoxCollider>());
        }
    }
    /*
    void OnTriggerEnter(Collision collision)
    {
        print("Trigger ahhhhhhhhhh");
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            print("tp");
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<BoxCollider>());
        }
    }
    */

}
