using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cactusgun : MonoBehaviour
{
    private double startTime;
    private double curr;
    public GameObject shot;
    private GameObject holder;
    public GameObject camera;
    private bool have = false;
    private const int maxAmmo = 7;
    private int shotsFired;
    private bool zpressed = false;
    private Vector3 startPoint;
    private Vector3 forwardRotation;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time - .5;
        shotsFired = 0;
        startPoint = transform.position;
        forwardRotation = new Vector3(-90, 0, -90);
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        curr = Time.time - startTime;
        if (Input.GetKey("g") && curr > .5 && have)
        {
            if (Input.GetKey("z")){
                Vector3 cpos = camera.transform.position;
                Vector3 cdir = camera.transform.forward;
                Quaternion crot = camera.transform.rotation;
                float spawnDistance = 3;
                Vector3 spawnp = cpos + cdir * spawnDistance;
                Instantiate(shot, spawnp, crot);
                zpressed = true;
            }
            else if(!zpressed){
                Instantiate(shot, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
            }
            else {
                
                Vector3 gpos = transform.position;
                Vector3 gdir = transform.right;
                Quaternion grot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(270, 0, 270));
                float spawnDistance = 3;
                Vector3 spawnp = gpos + gdir * spawnDistance;
                Instantiate(shot, spawnp, grot);
                
                //Instantiate(shot, transform.position + new Vector3(0, 0, 7), Quaternion.identity);
            }
            //Vector3 spawnPos = transform.position + transform.forward * 7;
            //Instantiate(shot, spawnPos, transform.rotation);
            //Instantiate(shot,holder.transform.position - holder.transform.forward * 7,
            //  Quaternion.Euler(new Vector3(holder.transform.rotation.eulerAngles.x, holder.transform.rotation.eulerAngles.y - 180, holder.transform.rotation.eulerAngles.z)));
            startTime = Time.time;
            shotsFired++;
        }
        if (have)
        {
            Vector3 temp = new Vector3(1f, 1f, 1f);
            transform.position = holder.transform.position + temp;
            if (!zpressed)
            {
                transform.rotation = Quaternion.Euler(forwardRotation);
            }
            //transform.position = holder.transform.position + new Vector3((float)1.5,(float)1.5, 0);
            //transform.rotation = Quaternion.Euler(forwardRotation);
            //transform.parent = transform;
            //transform.localPosition = new Vector3((float) 7.3,-10,(float)-102.6);
            //transform.rotation = Quaternion.Euler(holder.transform.rotation.eulerAngles + new Vector3(-90,-180,-90));
            //print(transform.rotation);
            if (Input.GetKey("z"))
            {
                transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles + new Vector3(-90, 0, -90));
                transform.position = holder.transform.position + new Vector3((float).5, (float).5, 0);
            }
            /*
            else
            {
                transform.position = holder.transform.position + new Vector3((float).5, (float).5, 0);
                transform.rotation = Quaternion.Euler(holder.transform.rotation.eulerAngles + forwardRotation);
                transform.parent = camera.transform;
            }
            */
        }
        if(shotsFired >= maxAmmo)
        {
            Instantiate(gameObject, startPoint, Quaternion.Euler(forwardRotation));
            Destroy(gameObject);
        }
    }
    /*
    void OnCollisionEnter(Collision c)
    {
        if (!have)
        {
            holder = c.gameObject;
            Vector3 pos = c.transform.position;
            Vector3 curob = transform.position;
            curob.x = pos.x + .5f;
            curob.y = pos.y + .5f;
            curob.z = pos.z - .5f;
            transform.position = curob;
            //transform.rotation = Quaternion.Euler(holder.transform.rotation.eulerAngles + new Vector3(90, 0, 90));
            have = true;
            BoxCollider bc = gameObject.GetComponent<BoxCollider>();
            bc.isTrigger = true;
        }
    }
   */
}
