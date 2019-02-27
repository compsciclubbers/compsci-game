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
    void Start()
    {
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
            if(type == "cactusgun")
            {
                double curr = Time.time - startTime;
                if (Input.GetKey("g") && curr > .5)
                {
                    Transform itT = transform;
                    Vector3 ipos = itT.position;
                    Vector3 idir = itT.right;
                    Quaternion irot = itT.rotation * Quaternion.Euler(0,90,90);
                    float spawnDistance = 3;
                    Vector3 spawnp = ipos + idir * spawnDistance;
                    Instantiate(helperItem, spawnp, irot);
                    startTime = Time.time;
                }
            }
            if(type == "stickymagnet")
            {

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

    
}
