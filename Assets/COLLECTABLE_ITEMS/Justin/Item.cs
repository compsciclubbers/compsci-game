using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;
    private bool equipped = false;
    private GameObject item;
    private GameObject itemManager;
    public GameObject helperItem;
    private double startTime;
    private string key;
    bool placed;
    Transform player;
    Vector3 playerPos;
    Animator anim;
    private bool isSwinging = false;
    void Start()
    {
        placed = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = transform.localPosition;
        key = "e";
        startTime = Time.time - .5;
        itemManager = GameObject.FindWithTag("Item Manager");
        if (!equipped)
        {
            int allItems = itemManager.transform.childCount;
            for (int i = 0; i < allItems; i++)
            {
                if (itemManager.transform.GetChild(i).gameObject.GetComponent<Item>().id == id)
                {
                    item = itemManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            double curr = Time.time - startTime;
            if (type == "cactusgun")
            {   
                if (Input.GetKey(key) && curr > .5)
                {
                    Transform itT = transform;
                    Vector3 ipos = itT.position;
                    Vector3 idir = itT.right;
                    Quaternion irot = itT.rotation * Quaternion.Euler(0,90,0);
                    float spawnDistance = 3;
                    Vector3 spawnp = ipos + idir * spawnDistance;
                    GameObject spawned = Instantiate(helperItem, spawnp, irot);
                    spawned.SetActive(true);
                    startTime = Time.time;
                }
            }
            if(type == "stickymagnet")
            {
                if (Input.GetKey(key) && curr > .5)
                {
                    float spawnDistance = 3;
                    GameObject spawned = Instantiate(helperItem, transform.GetComponentInParent<Transform>().GetComponentInParent<Transform>().position + transform.GetComponentInParent<Transform>().GetComponentInParent<Transform>().forward * 3, transform.GetComponentInParent<Transform>().GetComponentInParent<Transform>().rotation);
                    spawned.SetActive(true);
                    startTime = Time.time;
                }
            }
            if(type == "sword")
            {
                print("sword");
                isSwinging = anim.GetCurrentAnimatorStateInfo(0).IsName("Swing");
                if (Input.GetKey(key) && curr > .5)
                {
                    print("swing");
                    anim = GetComponent<Animator>();
                    anim.Play("Swing", -1, 0f);
                    startTime = Time.time;
                }
            }
            if(type == "shield")
            {
                
                if (Input.GetKey(key) && curr > .5)
                {
                    if (!placed)
                    {
                        GetComponent<BoxCollider>().isTrigger = false;
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        placed = true;
                    }
                    else
                    {
                        GetComponent<BoxCollider>().isTrigger = true;
                        GetComponent<Rigidbody>().useGravity = false;
                        transform.parent = itemManager.transform;
                        transform.localPosition = playerPos;
                        transform.rotation = player.rotation;
                        placed = false;
                    }
                    startTime = Time.time;
                }
            }
        }
    }

    public void itemUsage()
    {
        int allItems = itemManager.transform.childCount;
        for (int i = 0; i < allItems; i++)
        {
            itemManager.transform.GetChild(i).gameObject.SetActive(false);
        }
        item.SetActive(true);
        item.GetComponent<Item>().equipped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && type == "sword" && isSwinging == true)
        {
            print("hit");
        }
    }


}
