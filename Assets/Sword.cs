using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
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
}
