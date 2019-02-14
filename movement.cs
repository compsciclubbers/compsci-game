using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject shot;
   // public GameObject ;
    public int x = 1;
    public int y = 30;
    public int z = 20;
    private double startTime;
    private double curr;
    private double startTime2;
    private double curr2;
    // Start is called before the first frame update
    void Start()
    {
        startTime2 = Time.time - 2.5;
        startTime = Time.time - .5;
        gameObject.tag = "player";
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey("z")) { 
            curr2 = Time.time - startTime2;
            if (Input.GetKey("space") && curr2 > 2.5) {
                rb.AddForce(0, y, 0);
                startTime2 = Time.time;
            }
            if (Input.GetKey("right")) {
                rb.AddForce(x, 0, 0);
            }
            if (Input.GetKey("left"))
            {
                rb.AddForce(-x, 0, 0);
            }
            if (Input.GetKey("up"))
            {
                rb.AddForce(0, 0, z);
            }
            if (Input.GetKey("down"))
            {
                rb.AddForce(0, 0, -z);
            }
            curr = Time.time - startTime;
            if (Input.GetKey("s") && curr > .5)
            {
                Instantiate(shot, new Vector3(rb.transform.position.x, rb.transform.position.y, (rb.transform.position.z + 0x5)), Quaternion.identity);
                startTime = Time.time;
            }
        }
    }
}
