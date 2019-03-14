using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{

    [Header("AI settings")]
    public GameObject target;
    public float Speed;
    public float distanceUntilChase;

    void Update()
    {
        // if ((target.transform.position - this.transform.position).sqrMagnitude < distanceUntilChase)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
            transform.LookAt(target.transform);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().AddForce(0, 200, 0);
            GetComponent<Rigidbody>().AddForce(transform.forward * -200);
        }
        else if (collision.gameObject.tag != "Ground")
        {
            jumpOverObstacle();
        }
        
        
    }

    private void jumpOverObstacle()
    {
        GetComponent<Rigidbody>().AddForce(0, 500, 0);
    }
}