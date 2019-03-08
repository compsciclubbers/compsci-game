using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    public GameObject inventory;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    private Transform child;
    private int currIndex = 0;
    private double startTime;
    private double curr;
    void Start()
    {
        inventory.SetActive(false);
        allSlots = 9;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = inventory.transform.GetChild(i).gameObject;
            if(slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
        startTime = Time.time;
    }

    void Update()
    {
        if (inventory.active)
        {
            child = slot[currIndex].transform.GetChild(0).GetChild(0);
            child.gameObject.SetActive(true);
            for (int i = 0; i < allSlots; i++)
            {
                if(i != currIndex)
                {
                    slot[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
            curr = Time.time - startTime;
            if (Input.GetKey("left") && curr > .1)
            {
                if (currIndex == 0)
                {
                    currIndex = allSlots - 1;
                }
                else
                {
                    currIndex--;
                }
                startTime = Time.time;
            }
            if (Input.GetKey("right") && curr > .1)
            {
                if (currIndex == allSlots - 1)
                {
                    currIndex = 0;
                }
                else
                {
                    currIndex++;
                }
                startTime = Time.time;
            }

            if (Input.GetKey("return") && slot[currIndex].transform.childCount > 1)
            { 
                slot[currIndex].transform.GetChild(1).gameObject.GetComponent<Item>().itemUsage();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.active);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            if (GameObject.FindWithTag("Item Manager").transform.GetChild(0).GetComponent<Item>().type != item.type)
            {
                addItem(itemPickedUp, item.id, item.type, item.description, item.icon);
            }
        }
    }

    private void addItem(GameObject itemPickedUp, int id, string type, string description, Sprite icon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemPickedUp.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().icon = icon;
                slot[i].GetComponent<Slot>().id = id;
                slot[i].GetComponent<Slot>().type = type;
                slot[i].GetComponent<Slot>().description = description;
                slot[i].GetComponent<Slot>().item = itemPickedUp;
                itemPickedUp.transform.parent = slot[i].transform;
                itemPickedUp.SetActive(false);
                slot[i].GetComponent<Slot>().updateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return;
            }
            
        }
    }
}
