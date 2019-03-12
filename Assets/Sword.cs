using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnColliderEnter(Collision collision)
    {
        print("Collision ahhhh");
        if (collision.gameObject.tag == "Player")
        {
            print("cp");
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<BoxCollider>());
        }
    }
}
