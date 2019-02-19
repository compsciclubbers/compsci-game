using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollower : MonoBehaviour
{
    public GameObject player;
    private Vector3 camrot;
    private Rigidbody rb;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -10);
        transform.LookAt(player.transform);
        camrot = transform.rotation.eulerAngles;
        //rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z"))
        {
            if (count < 1)
            {                
                transform.rotation = Quaternion.Euler(camrot + new Vector3(-24, 0, 0));
                camrot = transform.rotation.eulerAngles;
                count++;
            }
            transform.position = player.transform.position + new Vector3(0, (float).5, (float).5);
            int range = 35;
            float xangle = transform.localEulerAngles.x;
            xangle = (xangle > 180) ? xangle - 360 : xangle;
            float yangle = transform.localEulerAngles.y;
            yangle = (yangle > 180) ? yangle - 360 : yangle;
            if (Input.GetKey("up") && xangle >= -range)
            {
                transform.rotation = Quaternion.Euler(camrot + new Vector3(-1, 0, 0));
                camrot = transform.rotation.eulerAngles;
            }
            //if (Input.GetKey("right") && yangle <= range)
            if (Input.GetKey("right"))
            {
                transform.rotation = Quaternion.Euler(camrot + new Vector3(0, 1, 0));
                camrot = transform.rotation.eulerAngles;
            }
            //if (Input.GetKey("left") && yangle >= -range)
            if (Input.GetKey("left"))
            {
                transform.rotation = Quaternion.Euler(camrot + new Vector3(0, -1, 0));
                camrot = transform.rotation.eulerAngles;
            }
            if (Input.GetKey("down") && xangle <= range)
            {
                transform.rotation = Quaternion.Euler(camrot + new Vector3(1, 0, 0));
                camrot = transform.rotation.eulerAngles;
            }
            
        }
        else
        {
            count = 0;
            camrot = transform.rotation.eulerAngles;
            transform.position = player.transform.position + new Vector3(0, 5, -10);
            transform.LookAt(player.transform);
        }
    }
}
