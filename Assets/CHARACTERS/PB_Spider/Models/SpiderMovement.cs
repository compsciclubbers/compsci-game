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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            jumpOverObstacle();
        }
        else if(collision.gameObject.tag == "Player")
        {
            transform.position = transform.forward * -1 * (float)Speed + transform.position;
        }
    }

    private void jumpOverObstacle()
    {
        transform.position = transform.up * (float)Speed * 20 + transform.position;
    }
}